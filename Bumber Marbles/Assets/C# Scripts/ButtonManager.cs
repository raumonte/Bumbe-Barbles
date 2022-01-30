using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exits");
    }
    //The scene manager gets the current active scene and adds one to go into the next scene in the scene index.
    public void NextScene()
    {
    }
}
