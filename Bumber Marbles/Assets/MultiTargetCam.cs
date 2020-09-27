using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTargetCam : MonoBehaviour
{
    public static MultiTargetCam instance;
    public List<Transform> targets;

    public Vector3 offset;
    public float smoothTime;
    public float minZoom = 40;
    public float maxZoom = 10f;

    private Vector3 velocity;
    private Camera cam;

    private void Awake()
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

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        //foreach(CharacterStats player in GameManager.instance.currentPlayers)
        //{
        //    targets.Add(player.transform);
        //}
        Move();
        Zoom();
    }
    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / 20f);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }
    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i <targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
