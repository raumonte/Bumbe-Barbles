using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject camera;
    private void Awake()
    {

    }
    /// <summary>
    /// Spawns player at spawn point
    /// </summary>
    /// <param name="playerPrefab">prefab to spawn</param>
    /// <param name="playerNum">Player num</param>
    public void SpawnPlayer(GameObject playerPrefab, int playerNum)
    {
        GameObject cameraOrbit = Instantiate(camera);
        GameObject spawnedPlayer = Instantiate(playerPrefab, transform.position, transform.rotation);
        spawnedPlayer.GetComponent<CharacterStats>().playerNumber = playerNum;
        spawnedPlayer.GetComponent<CharacterInputManager>().playerInputSpace = cameraOrbit.transform;
        spawnedPlayer.name = "Player " + playerNum;
        cameraOrbit.GetComponent<CameraOrbit>().focus = spawnedPlayer.transform;

        spawnedPlayer.GetComponent<CharacterStats>().camera = cameraOrbit.GetComponent<Camera>();
        CameraManager.instance.cameras.Add(cameraOrbit.GetComponent<Camera>());
        CameraManager.instance.UpdateScreen();
    }
}
