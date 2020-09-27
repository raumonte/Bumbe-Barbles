using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum powerUp { healthRegen, doubleSpeed, meteor, intangibility}
    public powerUp selectedPowerUp;
    private void Start()
    {
        selectedPowerUp = powerUp.healthRegen;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    // Check if the player is big enough to pick the object up.
    //    if (collision.transform.localScale.x > transform.localScale.x)
    //    {
    //        OnPickup(collision.gameObject);
    //        Destroy(gameObject);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        PickupAffect();  
    
    }
    void PickupAffect()
    {
        Debug.Log("Let Me Scream");
        Destroy(gameObject);
    }
    public void OnActivate(CharacterStats target)
    {
        target.ballTorque *= 2;
        target.dashForce *= 2;
    }
    public void OnDeactivate(CharacterStats target)
    {
        target.ballTorque /= 2;
        target.dashForce /= 2;
    }

    // Called when someone picks up this item.
    protected virtual void OnPickup(GameObject player)
    {
        // Do the thing.

    }
}
