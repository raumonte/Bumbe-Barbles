using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public enum GameState { StartScreen, GameRunning, EndScreen };
    [Header("Game Stats")]
    public GameObject playerPreb;
    public int startingNumOfPlayers = 2;
    [Tooltip("Current state the game is in")]
    public GameState currentGameState = GameState.StartScreen;
    [Tooltip("List of Current players")]
    public List<CharacterStats> currentPlayers = new List<CharacterStats>();
    [Tooltip("list of current powerups on the field")]
    public List<Pickup> currentPowerups = new List<Pickup>();
    [Tooltip("current pickup spawners")]
    public List<PickupSpawner> pickupSpawns = new List<PickupSpawner>();
    [Tooltip("Current list of player spawners")]
    public List<PlayerSpawner> playerSpawners = new List<PlayerSpawner>();

    [Header("UI Holders:")]
    public GameObject startScreen;
    public GameObject endScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.StartScreen;
        //set the instance gamemanager to this one
        
    }

    // Update is called once per frame
    void Update()
    {
        //while the game is in the game running state
        if (currentGameState == GameState.GameRunning)
        {
            startScreen.SetActive(false);
            endScreen.SetActive(false);
            //if there is less than 2 players
            if (currentPlayers.Count== 1)
            {
                currentGameState = GameState.EndScreen;
            }
        }
        else if (currentGameState == GameState.StartScreen)
        {
            startScreen.SetActive(true);
            endScreen.SetActive(false);
        }
        else
        {
            startScreen.SetActive(false);
            endScreen.SetActive(true);
        }

    }

}
