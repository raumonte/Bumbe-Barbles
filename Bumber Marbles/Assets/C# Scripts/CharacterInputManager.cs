using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ensure that the gameobject being set to has the needed components (if it doesn't, it'll add one)
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterStats))]
public class CharacterInputManager : MonoBehaviour
{
    //dash stuff
    bool canDash
    {
        get
        {
            return dashTimer <= 0;
        }
    }
    private float dashTimer;
    float attackTimer;

    //needed components
    private Rigidbody rBody;
    private CharacterStats stats;

    //physics variables
    private float currentSpeed;
    Vector3 lastposition;

    //input trackers
    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        //grab the needed component from the game object
        rBody = this.GetComponent<Rigidbody>();
        stats = this.GetComponent<CharacterStats>();

        //set last postion to start position
        lastposition = transform.position;

        //intialize the timers
        dashTimer = stats.dashCooldownTime;

        //set ball physic stats
        rBody.drag = stats.drag;
        rBody.mass = stats.mass;

       
    }

    // Update is called once per frame
    void Update()
    {
        // count down timers
        dashTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;

        //keep track of current speed
        currentSpeed = (transform.position - lastposition).magnitude / Time.deltaTime;
        lastposition = transform.position;

        //keep track of vertical and horizontal inputs 
        //(values range from [-1, 1]. For example, S in WASD will return a -1, W would return the opposite)
        horizontalInput = Input.GetAxis("Horizontal" + stats.playerNumber);
        verticalInput = Input.GetAxis("Vertical" + stats.playerNumber);


        //apply's torque to the ball based on inputs
        rBody.AddForce(new Vector3(horizontalInput, 0, verticalInput) * stats.moveForce * Time.deltaTime);


        //if player presses the space bar
        if (Input.GetButton("Fire" + stats.playerNumber))
        {
            Debug.Log("This is the player" + stats.playerNumber + " controller");
            //if the dash timer = 0
            if (canDash)
            {
                stats.isAttacking = true;
                attackTimer = stats.attackTime;
                //dash and reset timer
                rBody.AddForce(new Vector3(horizontalInput, 0, verticalInput) * stats.dashForce);
                dashTimer = stats.dashCooldownTime;
            }
        }

        if(attackTimer < 0)
        {
            stats.isAttacking = false;
        }
    }
}
