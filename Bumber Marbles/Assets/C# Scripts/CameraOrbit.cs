using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraOrbit : MonoBehaviour
{
    [SerializeField] Transform focus;
    [SerializeField] float distance = 5f;

    private void LateUpdate()
    {
        Vector3 focusPoint = focus.position;
        Vector3 lookDirection = transform.forward;
        transform.localPosition = focusPoint - lookDirection * distance;
    }
}
