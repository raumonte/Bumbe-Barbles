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
    public void NextScene()
    {
        //SceneManager.LoadScene(sceneBuildIndex + 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
