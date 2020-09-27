using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.playerSpawners.Add(this);
    }
    /// <summary>
    /// Spawns player at spawn point
    /// </summary>
    /// <param name="playerPrefab">prefab to spawn</param>
    /// <param name="playerNum">Player num</param>
    public void SpawnPlayer(GameObject playerPrefab, int playerNum)
    {
        GameObject spawnedPlayer = Instantiate(playerPrefab, transform.position, transform.rotation);
        spawnedPlayer.transform.FindChild("Player_visuals").GetComponent<CharacterStats>().playerNumber = playerNum;
        spawnedPlayer.name = "Player " + playerNum;
    }
}
