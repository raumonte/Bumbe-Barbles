using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum powerUp { healthRegen, doubleSpeed, meteor, intangibility}
    public powerUp selectedPowerUp;
    public bool IsPoweredDash;
    public float duration = 5f;
    private void Start()
    {
        selectedPowerUp = powerUp.healthRegen;
        IsPoweredDash = false;
    }
    private void OnCollisionEnter(Collision collision)
    {

        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    StartCoroutine( PickupAffect (collision) );
        //}
        //if (IsPoweredDash == false)
        //{
        //    IsPoweredDash = true;
        //    OnActivate(collision.gameObject.GetComponent<CharacterStats>());
        //}
        //else if (IsPoweredDash == true)
        //{
        //    OnDeactivate(collision.gameObject.GetComponent<CharacterStats>());
        //    IsPoweredDash = false;
        //}

        Destroy(this.gameObject);
    }
    //private void OnCollisionEnter(Collider other)
    //{
    //    //if (other.gameObject.CompareTag("Player"))
    //    //{
    //    //    PickupAffect();
    //    //}
    //    OnActivate(other.gameObject.GetComponent<CharacterStats>());

    //}
    //IEnumerator PickupAffect(Collision player)
    //{
    //    //CharacterStats stats = player.GetComponent<CharacterStats>();
    //    //stats.ballTorque *= 2;
    //    //stats.dashForce *= 2;
    //    //yield return new WaitForSeconds(duration);
    //    //stats.ballTorque /= 2;
    //    //stats.dashForce /= 2;
    //    //Debug.Log("Let Me Scream");
    //    //Destroy(gameObject);
    //}
    public void OnActivate(CharacterStats target)
    {
        target.ballTorque *= 2f;
        target.dashForce *= 2f;
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
