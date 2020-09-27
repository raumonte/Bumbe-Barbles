using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public GameManager instance;
    public enum GameState {StartScreen, GameRunning, EndScren};
    [Header("Game Stats")]
    public int startingNumOfPlayers = 4;
    [Tooltip("Current state the game is in")]
    public GameState currentGameState = GameState.StartScreen;
    [Tooltip("List of Current players")]
    List<CharacterStats> currentPlayers = new List<CharacterStats>();
    [Tooltip("list of current powerups on the field")]
    List<Pickup> currentPowerups = new List<Pickup>();
    [Tooltip("current pickup spawners")]
    List<PickupSpawner> pickupSpawns = new List<PickupSpawner>();
    [Tooltip("Current list of player spawners")]
    List<PlayerSpawner> playerSpawners = new List<PlayerSpawner>();

    [Header("UI Holders:")]
    public GameObject startScreen;
    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        //set the instance gamemanager to this one
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
       //while the game is in the game running state
       if(currentGameState == GameState.GameRunning)
        {
            //if there is less than 2 players
            if(currentPlayers.Count < 2)
            {
                currentGameState = GameState.EndScren;
            }
        }
    }

}
