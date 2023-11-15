using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : PowerUp
{
    [SerializeField] private float _fireRatePoints;
    protected override void GrantPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<Shooting>();
        if (shooting != null)
        {
            shooting.ChangePlayerFireRate(-_fireRatePoints);
        }
    }

    protected override void RevertPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<Shooting>();
        if (shooting != null)
        {
            shooting.ChangePlayerFireRate(_fireRatePoints);
        }
    }
}
