using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public GameObject debugObject;

    private void OnCollisionEnter(Collision collision)
    {
        // Store the other colliders stats.
        CharacterStats otherCharacterStats = collision.gameObject.GetComponent<CharacterStats>();

        // If they don't have one, then don't do anything.
        if (otherCharacterStats == null)
        {
            return;
        }


        Debug.Log(transform.localScale.x * gameObject.GetComponent<Rigidbody>().velocity.magnitude * Mathf.Clamp(Vector3.Dot(collision.gameObject.GetComponent<Rigidbody>().velocity, gameObject.GetComponent<Rigidbody>().velocity), 0, 1));

        // Calculate damage.
        otherCharacterStats.currentHealth -= transform.localScale.x * gameObject.GetComponent<Rigidbody>().velocity.magnitude * Mathf.Clamp(Vector3.Dot(collision.gameObject.GetComponent<Rigidbody>().velocity, gameObject.GetComponent<Rigidbody>().velocity), 0, 1);


    }
}
