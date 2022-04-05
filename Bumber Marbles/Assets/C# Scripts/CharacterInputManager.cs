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
    public ParticleSystem particleSystem;
    [SerializeField] Vector2 playerMoveInput;
    [SerializeField] Transform ball;
    Vector3 velocity, desiredVelocity;
    bool onGround;
    float minGroundDotProduct;
    Vector3 contactNormal, lastContactNormal;

    bool desiredJump;

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

     

        OnValidate();
    }

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        if(velocity.magnitude >= (stats.maxSpeed* 0.6))
        {
            stats.isAttacking = true;
            particleSystem.Play();
        }
        else
        {
            particleSystem.Stop();
            stats.isAttacking = false;
        }

        Debug.Log("Velocity magnitude: " + velocity.magnitude + "\nDesiredVelocity Magnitude: " + desiredVelocity.magnitude);
        
        // Jump button;

        InputHandler(stats.playerNumber);
        UpdateBall();
    }

    private void FixedUpdate()
    {
        UpdateState();
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
        AdjustVelocity();

        rBody.velocity = velocity;

        ClearState();

    }

    void UpdateState()
    {
        velocity = rBody.velocity;
        if(onGround)
        {
            contactNormal.Normalize();
        }
        else
        {
            contactNormal = Vector3.up;
        }
    }
    void Jump()
    {
        if (onGround)
        {
            float jumpspeed = Mathf.Sqrt(-2 * Physics.gravity.y * stats.jumpHeight);
            float alignedSpeed = Vector3.Dot(velocity, contactNormal);
            if(alignedSpeed> 0f)
            {
                jumpspeed = Mathf.Max(jumpspeed - alignedSpeed, 0f);
            }
            velocity.y += jumpspeed;
        }
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
        onGround = false;
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

    Vector3 ProjectOnContactPlane(Vector3 vector)
    {
        return vector - contactNormal * Vector3.Dot(vector, contactNormal);
    }

    void AdjustVelocity()
    {
        Vector3 xAxis = ProjectOnContactPlane(Vector3.right).normalized;
        Vector3 zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

        float currentX = Vector3.Dot(velocity, xAxis);
        float currentZ = Vector3.Dot(velocity, zAxis);

        float acceleration = onGround ? stats.maxAcceleration :stats.maxAirAcceleration;
        float maxSpeedChange = acceleration * Time.deltaTime;

        float newX =
            Mathf.MoveTowards(currentX, desiredVelocity.x, maxSpeedChange);
        float newZ =
            Mathf.MoveTowards(currentZ, desiredVelocity.z, maxSpeedChange);

        velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
    }
}
