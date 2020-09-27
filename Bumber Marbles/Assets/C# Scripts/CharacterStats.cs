using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Movement Settings:")]
    [Tooltip("The amount torque applied to the ball")]
    public float ballTorque = 100;
    [Tooltip("The amount of force applied to the ball when dashing")]
    public float dashForce = 100;

    [Header("Ball Physics Settings:")]
    [Tooltip("The amount of drag applied to the ball to help it stop moving. 0 - infinity")]
    public float drag = 1;
    [Tooltip("The mass of the ball")]
    public float mass = 1;

    [Header("Player Stats")]
    [Tooltip("The starting health of a player")]
    public float startingHealth = 100;
    [Tooltip("The current health of a player")]
    public float currentHealth;
    [Tooltip("The current score of a player")]
    public float score;
    [Tooltip("The time in seconds between dashes")]
    public float dashCooldownTime = 1;
    [Tooltip("Bool if the player is attacking or no")]
    public bool isAttacking;
    [Tooltip("How long the player is in IsAttacking state")]
    public float attackTime;
    public int playerNumber = 1;
    public enum playerState { MarbleForm, pumpkinForm };
    public playerState state;
    [Tooltip("The time in seconds when the player is out of the marble")]
    public float outOfMarbleTime;
    float marbleTimer;
    [Header("Player Components:")]
    public GameObject marble;
    public GameObject pumpkin;

    Rigidbody rgbd;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        //set stats
        currentHealth = startingHealth;
        rgbd.drag = drag;
        rgbd.mass = mass;
        state = playerState.MarbleForm;

        GameManager.instance.currentPlayers.Add(this);
    }

    private void Update()
    {
        //timer
        if (state == playerState.pumpkinForm && marbleTimer <= 0)
        {
            ChangeState(playerState.MarbleForm);
        }
        else
        {
            marbleTimer -= Time.deltaTime;
        }
        //keep track of states
        if (state == playerState.pumpkinForm)
            //keep track of the state that's its in
            switch (state)
            {
                case playerState.MarbleForm:
                    marble.SetActive(true);
                    break;
                case playerState.pumpkinForm:
                    marble.SetActive(false);
                    break;

            }
        
    }

    public void ChangeState(playerState changedState)
    {
        state = changedState;
        if (changedState == playerState.pumpkinForm)
        {
            marbleTimer = outOfMarbleTime;
        }
        else
        {
            currentHealth = startingHealth;
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.currentPlayers.Remove(this);
    }

}
