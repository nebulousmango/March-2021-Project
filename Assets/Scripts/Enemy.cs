using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //Variable to access Enemy's animator.
    Animator animator;

    #region Non AI
    //Variable to store Enemy's current health.
    int i_currentHealth;
    //Variable to set Enemy's total health.
    public int i_totalHealth = 100;
    //Variable to access Enemy's Health Bar script.
    HealthBar health;

    //Function to reduce enemy's health by player's weapon damage.
    public void ChangeHealth(int changeValue)
    {
        //Reduces Enemy health by assigned integer value.
        i_currentHealth += changeValue;
        //Sets range for Enemy's current health.
        i_currentHealth = Mathf.Clamp(i_currentHealth, 0, i_totalHealth);
        //Changes health bar's scale based on Enemy health.
        EditHealthBar();
        //Checks if Enemy is dead.
        CheckDie();
    }

    //Returns value to use by the HealthBar script's ScaleAnchor function.
    void EditHealthBar()
    {
        float healthfraction = (float)i_currentHealth / (float)i_totalHealth;
        health.ScaleAnchor(healthfraction);
    }

    //Runs death function if Enemy's health is 0.
    void CheckDie()
    {
        if (i_currentHealth <= 0)
        {
            //Runs Enemy death function.
            Die();
        }
    }

    //Function for when Enemy's health reaches 0.
    void Die()
    {
        agent.isStopped = true; StopAllCoroutines();
        //Trigger parameter used by Enemy animator.
        animator.SetTrigger("Death");
        //Runs coroutine to destroy Enemy object.
        StartCoroutine(DestroyDelay());
    }

    //Coroutine to destroy Enemy object after 2 seconds.
    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    #endregion

    #region AI
    //Variable to access Enemy's NavMeshAgent component.
    NavMeshAgent agent;
    //Variable to access NavMeshRoom script.
    NavMeshRoom room;
    //List variable to store waypoint positions.
    List<Vector3> v3_wptPositions = new List<Vector3>();
    //Variable to set Attack particle effect.
    public GameObject Go_AttackParticle;
    //Variable to set Enemy's attack damage.
    public int I_AttackDamage = 20;
    //Variable to set Enemy's attack length.
    public float F_AttackLifetime = 3;
    //Bool for whether Enemy's attack is active or not. 
    bool b_startedAttacking = false;
    bool b_idle = true;

    //Adds positions of Waypoint objects in array to another list.
    public void PopulateWaypointPositions(Waypoint[] waypoints)
    {
        foreach (Waypoint waypoint in waypoints)
        {
            v3_wptPositions.Add(waypoint.transform.position);
        }
    }

    //Moves Enemy to random Waypoint.
    void GoToRandomWaypoint()
    {
        int Rand = Random.Range(0, v3_wptPositions.Count - 1);
        agent.SetDestination(v3_wptPositions[Rand]);
    }

    //Reduces Player's health if they enter Enemy's Sphere Collider.
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.ChangeHealth(-I_AttackDamage);
        }
    }

    //Function for Enemy's attack sequence.
    void Attack()
    {
        //Starts Attack coroutine and plays Attack particle effect.
        b_startedAttacking = true;
        StartCoroutine(AttackCoroutine());
        GameObject currentParticle = GameObject.Instantiate(Go_AttackParticle, transform);
        Destroy(currentParticle, F_AttackLifetime);
    }

    //Coroutine for Enemy's attack.
    IEnumerator AttackCoroutine()
    {
        //Enables Enemy's Sphere Collider, waits for Attack particle effect to end, disables Sphere Collider and then changes state.
        GetComponentInChildren<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(F_AttackLifetime);
        GetComponentInChildren<SphereCollider>().enabled = false;
        ChangeToMoveState();
    }

    //Moves Enemy to a new Waypoint.
    public void ChangeToMoveState()
    {
        b_idle = false;
        GoToRandomWaypoint();
        b_startedAttacking = false;
    }

    void ChangeToIdleState()
    {
        b_idle = true;
        animator.SetTrigger("Idle");
    }
    #endregion

    private void Start()
    {
        //Returns Health Bar script from the Health Bar BG child object.
        health = GetComponentInChildren<HealthBar>();
        //Returns animator from the Enemy child object.
        animator = GetComponentInChildren<Animator>();
        //Sets Enemy current health to total health.
        i_currentHealth = i_totalHealth;
        //Returns value from Enemy's NavMeshAgent component. 
        agent = GetComponent<NavMeshAgent>();
        room = FindObjectOfType<NavMeshRoom>();
        ChangeToIdleState();
    }

    private void Update()
    {
        if(b_startedAttacking == false && agent.remainingDistance < 0.1f && b_idle == false)
        {
            Attack();
        }
    }
}