using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowController : MonoBehaviour
{
    // Increases the player's scale.
    public void Grow(float growthAmount)
    {
        transform.localScale += Vector3.one * growthAmount;
    }
}
