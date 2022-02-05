using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public List<Camera> cameras;

    private void Awake()
    {
        cameras = new List<Camera>();
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


    public void SetSplitScreen()
    {
        switch(cameras.Count)
        {
            case 2:
                cameras[0].rect = new Rect(0, 0, 0.5f, 1f);
                cameras[1].rect = new Rect(0.5f, 0, 0.5f, 1f);
                break;
            case 3:
                cameras[0].rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                cameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cameras[2].rect = new Rect(0f, 0, 1f, 0.5f);
                break;
            case 4:
                cameras[0].rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                cameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cameras[2].rect = new Rect(0f, 0, 1f, 0.5f);
                cameras[3].rect = new Rect(0.5f, 0, 1f, 0.5f);
                break;
        }
        
    }

}
