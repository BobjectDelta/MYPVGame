using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletPowerUp : PowerUp
{
    [SerializeField] private int _additionalBulletsAmount;
    protected override void GrantPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<Shooting>();
        if (shooting != null)
        {
            shooting.ChangeAdditionalBulletAmount(_additionalBulletsAmount);
        }
    }

    protected override void RevertPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<Shooting>();
        if (shooting != null)
        {
            shooting.ChangeAdditionalBulletAmount(-_additionalBulletsAmount);
        }
    }
}
