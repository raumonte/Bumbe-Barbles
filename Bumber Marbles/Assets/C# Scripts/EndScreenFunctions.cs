using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenFunctions : MonoBehaviour
{
    public Text titleText;
    private void OnEnable()
    {
        titleText.text = GameManager.instance.winner + " Wins!";
    }
    public void OnRestartClick()
    {
        //clear everthing
        foreach (Pickup pickup in GameManager.instance.currentPowerups)
        {
            Destroy(pickup.gameObject);
            GameManager.instance.currentPowerups.Remove(pickup);
        }
        GameManager.instance.currentPowerups.Clear();
        foreach(CharacterStats player in GameManager.instance.currentPlayers)
        {
            Destroy(player.gameObject);
            GameManager.instance.currentPlayers.Remove(player);
        }
        GameManager.instance.currentPlayers.Clear();

        //spawn players
        for (int i = 0; i < GameManager.instance.startingNumOfPlayers; i++)
        {
            GameManager.instance.playerSpawners[i].SpawnPlayer(GameManager.instance.playerPreb, i + 1);
        }
        GameManager.instance.currentGameState = GameManager.GameState.GameRunning;
    }
    public void OnQuitClick()
    {
        Application.Quit();
    }
}
