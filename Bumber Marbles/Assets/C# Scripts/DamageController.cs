using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public GameObject debugObject;
    private CharacterStats stats;
    private void Start()
    {
        stats = GetComponent<CharacterStats>();
    }
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

        //if attacking (dashed)
        if (stats.isAttacking)
        {
            // Calculate damage.
            otherCharacterStats.currentHealth -= transform.localScale.x * gameObject.GetComponent<Rigidbody>().velocity.magnitude * Mathf.Clamp(Vector3.Dot(collision.gameObject.GetComponent<Rigidbody>().velocity, gameObject.GetComponent<Rigidbody>().velocity), 0, 1);
            //check if you break their marble
            if(otherCharacterStats.currentHealth < 0)
            {
                otherCharacterStats.ChangeState(CharacterStats.playerState.pumpkinForm);
            }
            //if the other is attacking too
            if(otherCharacterStats.isAttacking)
            {
                stats.currentHealth -= transform.localScale.x * gameObject.GetComponent<Rigidbody>().velocity.magnitude * Mathf.Clamp(Vector3.Dot(collision.gameObject.GetComponent<Rigidbody>().velocity, gameObject.GetComponent<Rigidbody>().velocity), 0, 1);
                if (stats.currentHealth < 0)
                {
                    stats.ChangeState(CharacterStats.playerState.pumpkinForm);
                }
            }
        }

    }
}
