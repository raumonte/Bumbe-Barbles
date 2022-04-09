using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Movement Settings:")]
    public float maxSpeed = 10f;
    public float maxAcceleration = 10f, maxAirAcceleration = 1f;
    public float bounciness = 0.5f;
    public float jumpHeight = 2f;
    public float maxGroundAngle = 25f;
    public float ballRadius = 0.5f;
    public float ballAlignSpeed = 180f;
    public float maxTurnSpeed = 15f;
   
    [Tooltip("The amount of force applied to the ball when dashing")]
    public float dashForce = 100;
    [Tooltip("Movement speed of the pumpkin")]
    public float pumpkinMoveSpeed;
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
    [SerializeField] private MeshRenderer meshRenderer;

    Rigidbody rgbd;

    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        //set stats
        currentHealth = startingHealth;
        state = playerState.MarbleForm;

        MatchManager.instance.currentPlayers.Add(this);
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
        MatchManager.instance.currentPlayers.Remove(this);
    }

}
