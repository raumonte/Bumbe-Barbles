using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float torpueBoost;
    public float dashBoost;
    public float timeDuration;
    public bool isPermenant;
    public void OnActivate(CharacterStats data)
    {
        data.ballTorque *= torpueBoost;
        data.dashForce *= dashBoost;
    }
    public void OnDeactivate(CharacterStats data)
    {
        data.ballTorque /= torpueBoost;
        data.dashForce /= dashBoost;
    }
}
