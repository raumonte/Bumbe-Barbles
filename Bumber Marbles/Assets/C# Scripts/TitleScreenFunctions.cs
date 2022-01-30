using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleScreenFunctions : MonoBehaviour
{
    public Slider playerNumSlider;
    public string sceneToLoad;
    
    /// <summary>
    /// starts the game
    /// </summary>
   public void OnStartClick()
    {
        SceneLoader.instance.LoadScene(sceneToLoad);
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
