using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public static MatchManager instance;
    public List<PlayerSpawner> playerSpawners = new List<PlayerSpawner>();
    public List<CharacterStats> currentPlayers = new List<CharacterStats>();
    public List<Pickup> currentPickup = new List<Pickup>();
    public List<PickupSpawner> pickupSpawners = new List<PickupSpawner>();

    [SerializeField] CameraManager cameraManager;

    // Start is called before the first frame update
    void Awake()
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

    private void OnEnable()
    {
        for (int i = 0; i < GameManager.instance.startingNumOfPlayers; i++)
        {
            playerSpawners[i].SpawnPlayer(GameManager.instance.playerPreb, i + 1);
        }
    }
    private void Start()
    {
        
      
    }

    public void CheckWin()
    {
        if(currentPlayers.Count < 2)
        {
            GameManager.instance.winner = currentPlayers[0].name;
            OnWin();
        }
    }

    public void OnWin()
    {
        SceneLoader.instance.LoadScene("EndScreen");
    }
}
