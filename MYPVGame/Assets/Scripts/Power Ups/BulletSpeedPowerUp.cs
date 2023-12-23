using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpeedPowerUp : PowerUp
{
    [SerializeField] private float _shootingForcePoints;
    protected override void GrantPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<ShootingInput>().GetShootingType();
        if (shooting != null)
            shooting.ChangeShootingForce(_shootingForcePoints);
    }

    protected override void RevertPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<ShootingInput>().GetShootingType();
        if (shooting != null)
            shooting.ChangeShootingForce(-_shootingForcePoints);
    }
}
