using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyPowerUp : PowerUp
{
    [SerializeField] private float _spreadDegreesPoints;
    protected override void GrantPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<ShootingInput>().GetShootingType();
        if (shooting != null)
            shooting.ChangeShootingSpread(-_spreadDegreesPoints);
    }

    protected override void RevertPowerUp(GameObject powerUpRecipient)
    {
        Shooting shooting = powerUpRecipient.GetComponent<ShootingInput>().GetShootingType();
        if (shooting != null)
            shooting.ChangeShootingSpread(_spreadDegreesPoints);
    }
}
