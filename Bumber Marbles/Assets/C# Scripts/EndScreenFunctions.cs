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
        SceneLoader.instance.LoadScene("MainMenu");
    }
    public void OnQuitClick()
    {
        Application.Quit();
    }
}
