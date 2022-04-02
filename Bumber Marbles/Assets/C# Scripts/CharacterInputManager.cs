using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ensure that the gameobject being set to has the needed components (if it doesn't, it'll add one)
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterStats))]
public class CharacterInputManager : MonoBehaviour
{
    public Transform playerInputSpace = default;
    [SerializeField] Vector2 playerMoveInput;
    [SerializeField] Transform ball;
    Vector3 velocity, desiredVelocity;
    bool onGround;
    float minGroundDotProduct;
    Vector3 contactNormal, lastContactNormal;


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

    private void Awake()
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

        OnValidate();
    }

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        ////if player presses the space bar
        //if (Input.GetButton("Fire" + stats.playerNumber))
        //{
        //    ("This is the player" + stats.playerNumber + " controller");
        //    //if the dash timer = 0
        //    if (canDash)
        //    {
        //        stats.isAttacking = true;
        //        attackTimer = stats.attackTime;
        //        //dash and reset timer
        //        rBody.AddForce(new Vector3(horizontalInput, 0, verticalInput) * stats.dashForce);
        //        dashTimer = stats.dashCooldownTime;
        //    }
        //}

        //if(attackTimer < 0)
        //{
        //    stats.isAttacking = false;
        //}

        InputHandler(stats.playerNumber);
        UpdateBall();
    }

    private void FixedUpdate()
    {
        PhysicsHandler();
    }

    void InputHandler(int playerNum)
    {
        playerMoveInput.x = Input.GetAxis("Horizontal" + playerNum);
        playerMoveInput.y = Input.GetAxis("Vertical" + playerNum);
        playerMoveInput = Vector2.ClampMagnitude(playerMoveInput, 1f);

        if (playerInputSpace)
            desiredVelocity = playerInputSpace.TransformDirection(playerMoveInput.x, 0, playerMoveInput.y) * stats.maxSpeed;
        else
            desiredVelocity = new Vector3(playerMoveInput.x, 0f, playerMoveInput.y) * stats.maxSpeed;

    }

    void PhysicsHandler()
    {
        rBody.velocity = velocity;

        float acceleration = onGround ? stats.maxAcceleration : stats.maxAirAcceleration;
        float maxSpeedChange = acceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        rBody.velocity = velocity;

        onGround = false;
        ClearState();

    }

    void Jump()
    {
        velocity += Mathf.Sqrt(-2 * Physics.gravity.y * stats.jumpHeight) * contactNormal;
    }

    void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

    void EvaluateCollision(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            if (normal.y >= minGroundDotProduct)
            {
                onGround = true;
                contactNormal += normal;
            }
        }
    }

    private void OnValidate()
    {
        minGroundDotProduct = Mathf.Cos(stats.maxGroundAngle * Mathf.Deg2Rad);
    }

    void ClearState()
    {
        lastContactNormal = contactNormal;
    }

    void UpdateBall()
    {
        Vector3 movement = rBody.velocity * Time.deltaTime;
        float distance = movement.magnitude;
        if (distance < 0.001f)
        {
            return;
        }
        float angle = distance * (180f / Mathf.PI) / stats.ballRadius;
        Vector3 rotationAxis =
            Vector3.Cross(lastContactNormal, movement).normalized;
        Quaternion rotation =
            Quaternion.Euler(rotationAxis * angle) * ball.localRotation;
        if (stats.ballAlignSpeed > 0f)
        {
            rotation = AlignBallRotation(rotationAxis, rotation, distance);
        }
        ball.localRotation = rotation;
    }
    Quaternion AlignBallRotation(Vector3 rotationAxis, Quaternion rotation, float traveledDistance)
    {
        Vector3 ballAxis = ball.up;
        float dot = Mathf.Clamp(Vector3.Dot(ballAxis, rotationAxis), -1f, 1f);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        float maxAngle = stats.ballAlignSpeed * Time.deltaTime;

        Quaternion newAlignment =
            Quaternion.FromToRotation(ballAxis, rotationAxis) * rotation;
        if (angle <= maxAngle)
        {
            return newAlignment;
        }
        else
        {
            return Quaternion.SlerpUnclamped(
                rotation, newAlignment, maxAngle / angle
            );
        }
    }
}
