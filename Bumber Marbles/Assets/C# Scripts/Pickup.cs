using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //public enum powerUps { healthRegen, doubleSpeed, meteor, intangibility}
    public PowerUp powerUp;
    public float duration = 5f;
    private Transform kamera;
    private void Start()
    {
      //  powerUp = powerUps.healthRegen;
        GameManager.instance.currentPowerups.Add(this);
        kamera = MultiTargetCam.instance.transform;

    }
    private void Update()
    {
        transform.LookAt(kamera, Vector3.up);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 origin = new Vector3(0, 0, 0);
        PowerUpController powerUpController = collision.gameObject.GetComponent<PowerUpController>();
        if (powerUpController != null)
        {
            powerUpController.AddPowerup(powerUp);
            Destroy(this.gameObject);
        }
    }
    void OnDestroy()
    {
        GameManager.instance.currentPowerups.Remove(this);
    }
}
