using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpeedPowerUp : PowerUp
{
    [SerializeField] private float _shootingForcePoints;
    protected override void GrantPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<Shooting>();
        if (shooting != null)
        {
            shooting.ChangePlayerShootingForce(_shootingForcePoints);
        }
    }

    protected override void RevertPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<Shooting>();
        if (shooting != null)
        {
            shooting.ChangePlayerShootingForce(-_shootingForcePoints);
        }
    }
}
