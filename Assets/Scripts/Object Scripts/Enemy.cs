using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //Variable to access Enemy's animator.
    Animator animator;

    #region Non AI
    //Variable to set Enemy's total health.
    public int i_totalHealth = 100;
    //Variable to set Enemy's Death particle effect.
    public GameObject Go_DeathParticle;
    //Variable to access Enemy's Health Bar script.
    HealthBar health;
    //Variable to access NavMeshRoom script.
    NavMeshRoom room;
    //Variable to store Enemy's current health.
    int i_currentHealth;
    //Bool to check whether Enemy has died.
    bool b_EnemyIsDead = false;

    //Function to reduce enemy's health by player's weapon damage.
    public void ChangeHealth(int changeValue)
    {
        //Reduces Enemy health by assigned integer value.
        i_currentHealth += changeValue;
        //Sets range for Enemy's current health.
        i_currentHealth = Mathf.Clamp(i_currentHealth, 0, i_totalHealth);
        //Changes health bar's scale based on Enemy health.
        EditHealthBar();
        //Runs CheckDie only if Enemy is still alive.
        if(b_EnemyIsDead == false)
        {
            //Checks if Enemy is dead.
            CheckDie();
        }
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
            //Changes bool value so that CheckDie doesn't run again on the same Enemy.
            b_EnemyIsDead = true;
        }
    }

    //Function for when Enemy's health reaches 0.
    void Die()
    {
        //Stops Enemy's movement and script's coroutines.
        agent.isStopped = true; StopAllCoroutines();
        //Plays the Enemy Death sound effect through the AudioManager script.
        FindObjectOfType<AudioManager>().PlaySound("Enemy Death");
        //Trigger parameter used by Enemy animator.
        animator.SetTrigger("Death");
        //Runs coroutine to destroy Enemy object.
        StartCoroutine(DestroyDelay());
    }

    //Coroutine to destroy Enemy object after 2 seconds.
    IEnumerator DestroyDelay()
    {
        //Plays Enemy's Death particle effect.
        GameObject currentParticle = GameObject.Instantiate(Go_DeathParticle, transform);
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
        //Adds to count of room's dead Enemies.
        room.ChangeDeadCount();
        //Checks if all Enemies are dead.
        room.CheckRoomEmpty();
    }
    #endregion

    #region AI
    //Variable to set Attack particle effect.
    public GameObject Go_AttackParticle;
    //Variable to set Enemy's attack damage.
    public int I_AttackDamage = 20;
    //Variable to set Enemy's attack length.
    public float F_AttackLifetime = 3;
    //Variable to access Enemy's NavMeshAgent component.
    NavMeshAgent agent;
    //List variable to store waypoint positions.
    List<Vector3> v3_wptPositions = new List<Vector3>();
    //Bool for whether Enemy's attack is active or not. 
    bool b_startedAttacking = false;
    //Bool for whether Enemy is in idle state or not.
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
        int Rand = Random.Range(0, v3_wptPositions.Count);
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
        b_startedAttacking = true;
        //Starts Enemy's Attack animation.
        animator.SetTrigger("Attack");
        //Starts Attack coroutine.
        StartCoroutine(AttackCoroutine());
        //Plays Attack particle effect.
        GameObject currentParticle = GameObject.Instantiate(Go_AttackParticle, transform);
        Destroy(currentParticle, F_AttackLifetime);
    }

    //Coroutine for Enemy's attack.
    IEnumerator AttackCoroutine()
    {
        //Enables Enemy's Sphere Collider, waits for Attack particle effect to end, disables Sphere Collider and then changes to move state.
        GetComponentInChildren<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(F_AttackLifetime);
        GetComponentInChildren<SphereCollider>().enabled = false;
        ChangeToMoveState();
    }

    //Function for Enemy's move state.
    public void ChangeToMoveState()
    {
        //Changes bool values used in Update and runs Enemy's Movement function.
        b_idle = false;
        GoToRandomWaypoint();
        b_startedAttacking = false;
    }

    //Function for Enemy's idle state.
    void ChangeToIdleState()
    {
        //Turns off Enemy's Attack Collider and enables Idle animation.
        b_idle = true;
        GetComponentInChildren<SphereCollider>().enabled = false;
        animator.SetTrigger("Idle");
    }
    #endregion

    private void Start()
    {
        //Returns Health Bar script from the Health Bar BG child object.
        health = GetComponentInChildren<HealthBar>();
        //Returns animator from the Enemy child object.
        animator = GetComponentInChildren<Animator>();
        //Returns value from Enemy's NavMeshAgent component. 
        agent = GetComponent<NavMeshAgent>();
        //Returns NavMeshRoom script.
        room = GetComponentInParent<NavMeshRoom>();
        //Sets Enemy current health to total health.
        i_currentHealth = i_totalHealth;
        ChangeToMoveState();
    }

    private void Update()
    {
        //Starts Attack function if Enemy is in move state and at its waypoint.
        if(b_startedAttacking == false && agent.remainingDistance < 0.1f && b_idle == false)
        {
            Attack();
        }
    }
}