using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variable to access Player's Rigidbody.
    Rigidbody rb;
    //Variable to access Player's Animator.
    Animator animator;

    #region Camera
    //Variable to set camera's horizontal movement speed.
    public float f_horizontalCameraSpeed = 4;
    //Variable to set camera's vertical movement speed.
    public float f_verticalCameraSpeed = 4;
    //Variable to access Main Camera's Camera component.
    Camera cam;

    //Function for camera movement.
    void CameraRotate()
    {
        //Stores the mouse's movement on the X axis, multiplied into the camera's horizontal speed.
        float f_h = Input.GetAxis("Mouse X") * f_horizontalCameraSpeed;
        //Stores the mouse's movement on the Y axis, multiplied into the camera's vertical speed.
        float f_v = Input.GetAxis("Mouse Y") * f_verticalCameraSpeed;
        //Rotates the Player object and Camera child object on the Y axis.
        transform.Rotate(0, f_h, 0);
        //Rotates the Camera child object on the X axis.
        cam.transform.Rotate(f_v, 0, 0);
        //Stores Camera's X axis Euler angle.
        Vector3 v3_camAngle = new Vector3(cam.transform.localEulerAngles.x, 0, 0);
        //Sets Camera's X axis rotation to 0 if the player looks up past 200 degrees.
        if (v3_camAngle.x > 200) v3_camAngle.x = 0;
        //Sets minimum and maximum values for Camera's X axis rotation.
        v3_camAngle.x = Mathf.Clamp(v3_camAngle.x, 0, 40);
        //Sets Camera's Euler angles to limited X axis values.
        cam.transform.localEulerAngles = v3_camAngle;
    }
    #endregion

    #region Movement
    //Variable to set Player's base movement speed.
    public float f_baseMoveSpeed;
    //Variable to multiply Player's base speed by.
    public float f_sprintMult;
    //Variable for Player's current movement speed.
    float f_currentMoveSpeed;
    //Variable for Player's sprint speed.
    float f_sprintSpeed;

    //Sets X and Y motion values for animator component.
    void MovementAnims()
    {
        animator.SetFloat("X Motion", Input.GetAxis("Horizontal"));
        animator.SetFloat("Y Motion", Input.GetAxis("Vertical"));
    }

    //Function for player movement.
    void MovePlayer()
    {
        //Uses input from movement keys to set X and Z velocity values.
        Vector3 v3_movement = (transform.right * Input.GetAxis("Horizontal") * f_currentMoveSpeed) + (transform.forward * Input.GetAxis("Vertical") * f_currentMoveSpeed) + (transform.up * rb.velocity.y);
        MovementAnims();
        rb.velocity = v3_movement;
    }

    //Function for player sprinting.
    void SprintPlayer()
    {
        //Changes Player to sprint speed if Left Shift is down.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            f_currentMoveSpeed = f_sprintSpeed;
        }
        //Returns Player to base speed when Left Shift is up.
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            f_currentMoveSpeed = f_baseMoveSpeed;
        }
    }

    //Function for updating Player's two speeds.
    void UpdateSpeed()
    {
        //Sets Player's current speed to base speed at Start.
        f_currentMoveSpeed = f_baseMoveSpeed;
        //Sets Player's sprint speed.
        f_sprintSpeed = f_baseMoveSpeed * f_sprintMult;
    }
    #endregion

    #region Jump
    //Variable to multiply Player's jumping height by.
    public float f_jumpForce;
    //Boolean for whether Player is on the floor or not.
    bool b_TouchingFloor = false;

    //Sets bool to true if Player is in contact with a Floors layer object.
    private void OnCollisionEnter(Collision other)
    {
        TouchingFloor(other, true, 6);
    }

    //Sets bool to false if Player is not in contact with a Floors layer object.
    private void OnCollisionExit(Collision other)
    {
        TouchingFloor(other, false, 6);
    }

    //Function for changing bool value based on layer.
    void TouchingFloor(Collision other, bool TrueFalse, int layer)
    {
        //Checks if Player is touching an object in a specific layer. 
        if (other.gameObject.layer == layer)
        {
            //Sets bool value to true or false based on value when called.
            b_TouchingFloor = TrueFalse;
        }
    }

    //Function for player jumping.
    void JumpPlayer()
    {
        //If the spacebar is down and Player is in contact with the floor.
        if (Input.GetKeyDown(KeyCode.Space) && b_TouchingFloor)
        {
            //Adds an upward force to Player based on assigned jump force value.
            rb.AddForce(transform.up * f_jumpForce);
            //Sets bool for Player's jump animation to true.
            animator.SetBool("AnimCtrl_Jump", true);
        }
        //If the spacebar is up and Player is not in contact with the floor.
        if (!Input.GetKeyDown(KeyCode.Space) && b_TouchingFloor)
        {
            //Sets bool for Player's jump animation to false.
            animator.SetBool("AnimCtrl_Jump", false);
        }
    }
    #endregion

    #region Attack
    //Variable to access Player's sword's Box Collider.
    public BoxCollider swordCollider;
    //Variable to set Player's sword's damage.
    public int I_WeaponDamage;

    //Function for when Player's sword comes in contact with Enemy object.
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() == true)
        {
            //Runs DamageEnemy function from the Enemy script.
            Enemy dancer = other.GetComponent<Enemy>();
            dancer.ChangeHealth(-I_WeaponDamage);
        }
    }

    //Function for turning Player's sword's Box Collider on and off.
    void SwordTriggerOnOff(bool OnOff)
    {
        swordCollider.enabled = OnOff;
    }

    //Function to set SwordTriggerOnOff value.
    void CheckHit()
    {
        //When left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            //Runs coroutine to turn on sword's Collider.
            StartCoroutine(SlashDelay());
            //Triggers parameter used by Player's attack animation.
            animator.SetTrigger("AnimCtrl_Attack");
        }
    }
    
    //Coroutine to turn sword's Collider on and off after time delays.
    IEnumerator SlashDelay()
    {
        //Waits for 0.3 seconds, then turns sword's Collider on.
        yield return new WaitForSeconds(0.3f);
        SwordTriggerOnOff(true);
        //Turns sword's Collider off after 0.4 seconds.
        yield return new WaitForSeconds(0.4f);
        SwordTriggerOnOff(false);
    }
    #endregion

    #region Health
    //Variable to set Player's total health.
    public int i_playerTotalHealth;
    //Variable to set Player's Death length.
    public float F_DeathLifetime = 1;
    //Variable to access Player's Health Bar script.
    public HealthBar health;
    //Variable to set Death particle effect.
    public GameObject Go_DeathParticle;
    //Variable to access RetryMenu script.
    public RetryMenu retryMenu;
    //Variable to store Player's current health.
    int i_playerCurrentHealth;
    //Bool for whether Player is alive or not.
    bool b_alive = true;

    //Changes Player's health based on input.
    public void ChangeHealth(int healthChange)
    {
        //Increases Player's current health by function's parameter.
        i_playerCurrentHealth += healthChange;
        //Sets range for Player's current health.
        i_playerCurrentHealth = Mathf.Clamp(i_playerCurrentHealth, 0, i_playerTotalHealth);
        //Edits Player's health bar.
        EditHealthBar();
        //Checks if Player is dead.
        CheckDeath();
    }

    //Restores Player's health. 
    void RestoreHealth()
    {
        //Changes Player's health to full if R is pressed.
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeHealth(i_playerTotalHealth);
        }
    }

    //Returns value to use by the HealthBar script's ScaleAnchor function.
    void EditHealthBar()
    {
        float healthfraction = (float)i_playerCurrentHealth / (float)i_playerTotalHealth;
        health.ScaleAnchor(healthfraction);
    }

    //Checks if Player's health has reached 0 while alive.
    void CheckDeath()
    {
        if (i_playerCurrentHealth <= 0 && b_alive) Die();
    }

    //Runs when Player is dead.
    void Die()
    {
        b_alive = false;
        //Starts Death coroutine.
        StartCoroutine(DeathCoroutine());
    }

    //Coroutine for when Player dies.
    IEnumerator DeathCoroutine()
    {
        //Plays and then destroys Death particle effect.
        GameObject currentParticle = GameObject.Instantiate(Go_DeathParticle, transform);
        Destroy(currentParticle, F_DeathLifetime);
        //Triggers parameter used by Player's death animation.
        animator.SetTrigger("Death");
        //Waits for particle effect to complete.
        yield return new WaitForSeconds(F_DeathLifetime);
        //Runs RetryMenu script's Retry function.
        retryMenu.Retry();
    }
    #endregion

    //Start runs on the first frame.
    private void Start()
    {
        //Returns the parent Player object's Rigidbody component.
        rb = GetComponent<Rigidbody>();
        //Return's the Main Camera object's Camera component.
        cam = GetComponentInChildren<Camera>();
        //Returns the Player model's animator component.
        animator = GetComponentInChildren<Animator>();
        //Sets Player's current health to total health.
        i_playerCurrentHealth = i_playerTotalHealth;
        //Turns off the Player's sword's Box Collider component. 
        SwordTriggerOnOff(false);
        //Sets Player's current and sprinting movement speeds.
        UpdateSpeed();
        //Freezes Player's rotation to prevent random spinning.
        rb.freezeRotation = true;
    }

    //Update is called every frame.
    void Update()
    {
        //When Player is alive.
        if(b_alive)
        {
            //Calls function for player movement.
            MovePlayer();
            //Calls function for player jumping.
            JumpPlayer();
            //Calls function for player sprinting.
            SprintPlayer();
            //Calls function for running SwordTriggerOnOff if player uses left mouse button.
            CheckHit();
            //Calls function for camera movement.
            CameraRotate();
            //Calls function to restore player health.
            RestoreHealth();
        }
    }
}
