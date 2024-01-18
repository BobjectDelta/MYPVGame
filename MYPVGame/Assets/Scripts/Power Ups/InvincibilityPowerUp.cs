using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvincibilityPowerUp : PowerUp
{
    protected override void GrantPowerUp(GameObject powerUpRecipient)
    {
        Health health = powerUpRecipient.GetComponent<Health>();
        if (health != null)
        {
            if (health.GetIsInvincible())
                health.StopInvincibilityCoroutine();
            StartCoroutine(health.SetInvincibility(_effectTime));
        }
    }

    protected override void RevertPowerUp(GameObject powerUpRecipient)
    {
        
    }
}
