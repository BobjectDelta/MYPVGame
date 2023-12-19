using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUp : PowerUp
{
    [SerializeField] private float _damagePoints;
    protected override void GrantPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<ShootingInput>().GetShootingType();
        if (shooting != null)
            shooting.ChangePlayerBulletDamage(_damagePoints);
    }

    protected override void RevertPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<ShootingInput>().GetShootingType();
        if (shooting != null)      
            shooting.ChangePlayerBulletDamage(-_damagePoints);
    }
}
