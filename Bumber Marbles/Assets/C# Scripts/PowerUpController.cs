using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private CharacterStats data;

    public List<PowerUp> powerUps;

    void Start()

    {

        data = GetComponent<CharacterStats>();

        powerUps = new List<PowerUp>();

    }
    private void Update()
    {
        List<PowerUp> expiredPowerups = new List<PowerUp>();
            foreach(PowerUp power in powerUps)
        {
            power.timeDuration -= Time.deltaTime;
            if(power.timeDuration <= 0)
            {
                expiredPowerups.Add(power);
            }
            foreach (PowerUp expiredPower in expiredPowerups)
            {
                expiredPower.OnDeactivate(data);
                powerUps.Remove(expiredPower);
            }
            expiredPowerups.Clear();
        }
    }
    public void AddPowerup(PowerUp power)
    {
        power.OnActivate(data);

        if (!power.isPermenant)
        {
            powerUps.Add(power);
        }
    }

}
