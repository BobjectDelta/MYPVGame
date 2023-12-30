using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletPowerUp : PowerUp
{
    [SerializeField] private int _additionalBulletsAmount;
    protected override void GrantPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<ShootingInput>().GetShootingType();
        if (shooting != null)      
            shooting.AddAdditionalBullet(_additionalBulletsAmount);
    }

    protected override void RevertPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<ShootingInput>().GetShootingType();
        if (shooting != null)
            shooting.RemoveAdditionalBullet(_additionalBulletsAmount);
    }
}
