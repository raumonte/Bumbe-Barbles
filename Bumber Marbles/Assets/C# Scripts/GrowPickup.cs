using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPickup : Pickup
{
    // Variables

    // The amount the player grows when picked up
    [SerializeField]
    private float growthAmount = 1f;

    protected override void OnPickup(GameObject player)
    {
        // Tell the player to grow.
        player.GetComponent<GrowController>().Grow(growthAmount);
    }
}
