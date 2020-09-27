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
    public int playerNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        //set current health to starting health at start
        currentHealth = startingHealth;

    }

}
