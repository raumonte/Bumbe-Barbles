using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum powerUpType { meteor, ghost}
    public powerUpType powerUp;
    public float torpueBoost;
    public float dashBoost;
    public float timeDuration;
    public bool isPermenant;
    public void OnActivate(CharacterStats data)
    {
        if (powerUp == powerUpType.meteor)
        {
            data.ballTorque *= torpueBoost;
            data.dashForce *= dashBoost;
        }
        else
        {
            data.gameObject.layer = 9;
        }
    }
    public void OnDeactivate(CharacterStats data)
    {
        if (powerUp == powerUpType.meteor)
        {
            data.ballTorque /= torpueBoost;
            data.dashForce /= dashBoost;
        }
        else
        {
            data.gameObject.layer = 8;
        }
    }
}
