using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleScreenFunctions : MonoBehaviour
{
    public Slider playerNumSlider;
    /// <summary>
    /// starts the game
    /// </summary>
   public void OnStartClick()
    {
        for (int i = 0; i < GameManager.instance.startingNumOfPlayers; i++)
        {
            GameManager.instance.playerSpawners[i].SpawnPlayer(GameManager.instance.playerPreb, i + 1);
        }
        GameManager.instance.currentGameState = GameManager.GameState.GameRunning;
    }
    public void OnSliderChange()
    {
        GameManager.instance.startingNumOfPlayers = (int)playerNumSlider.value;
    }
    public void OnQuitClick()
    {
        Application.Quit();
    }
}
