using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStick : MonoBehaviour
{
    public GameObject healthBar;
    public Transform cam;
    // Update is called once per frame
    //void Update()
    //{

      //  transform.position = new Vector3(this.GetComponentInParent<Transform>().position.x,1, this.GetComponentInParent<Transform>().position.z);
       //transform.LookAt(transform.position + cam.forward);
    //}
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bring"))
        {
            Instantiate(healthBar, transform.position, Quaternion.identity, transform);
        }
    }
}
