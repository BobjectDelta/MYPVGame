using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : PowerUp
{
    [SerializeField] private float _fireRatePoints;
    protected override void GrantPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<ShootingInput>().GetShootingType();
        if (shooting != null)
            shooting.ChangeFireRate(-_fireRatePoints);
    }

    protected override void RevertPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<ShootingInput>().GetShootingType();
        if (shooting != null)
            shooting.ChangeFireRate(_fireRatePoints);
    }
}
