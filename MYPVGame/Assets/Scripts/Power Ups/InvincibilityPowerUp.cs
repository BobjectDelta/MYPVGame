using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerUp : PowerUp
{
    protected override void GrantPowerUp(GameObject powerUpRecipient)
    {
        Health health = powerUpRecipient.GetComponent<Health>();
        if (health != null)
        {
            StartCoroutine(health.SetInvincibility(_effectTime));
        }
    }

    protected override void RevertPowerUp(GameObject powerUpRecipient)
    {
        
    }
}
