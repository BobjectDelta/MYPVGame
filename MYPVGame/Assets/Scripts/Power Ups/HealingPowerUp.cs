using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealingPowerUp : PowerUp
{
    [SerializeField] private float _healPoints;
    protected override void GrantPowerUp(GameObject healingRecipient)
    {
        Health health = healingRecipient.GetComponent<Health>();
        if (!_isTimed)       
            health.Heal(_healPoints);     
        else
            health.Heal(_healPoints * Time.deltaTime);                
    }

    protected override void RevertPowerUp(GameObject healingRecipient) 
    {
        Health health = healingRecipient.GetComponent<Health>(); 
        health.TakeDamage(health.GetHealth() - Mathf.RoundToInt(health.GetHealth()));
    }
}
