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
    [Tooltip("Movement speed of the pumpkin")]
    public float pumpkinMoveSpeed;

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
    public Color[] colors = new Color[4];
    [Header("Player Components:")]
    public GameObject marble;
    public GameObject pumpkin;
    public List<Material> marblePhasesMaterials = new List<Material>();
    private MeshRenderer meshRenderer;

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
        meshRenderer = marble.GetComponent<MeshRenderer>();
        meshRenderer.material.SetColor("_Color", colors[playerNumber-1]);
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
        //keep track of marble phases
        if((currentHealth/startingHealth) >= .99)
        {
            meshRenderer.material = marblePhasesMaterials[0];
            meshRenderer.material.SetColor("_Color", colors[playerNumber - 1]);
        }
        else if((currentHealth / startingHealth) < .99 && (currentHealth / startingHealth) >= .66)
        {
            meshRenderer.material = marblePhasesMaterials[1];
            meshRenderer.material.SetColor("_Color", colors[playerNumber - 1]);
        }
        else if ((currentHealth / startingHealth) < .66 && (currentHealth / startingHealth) >= .33)
        {
            meshRenderer.material = marblePhasesMaterials[2];
            meshRenderer.material.SetColor("_Color", colors[playerNumber - 1]);
        }
        else if ((currentHealth / startingHealth) < .33 && (currentHealth / startingHealth) >= 0)
        {
            meshRenderer.material = marblePhasesMaterials[3];
            meshRenderer.material.SetColor("_Color", colors[playerNumber - 1]);
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
