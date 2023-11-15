using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedPowerUp : PowerUp
{
    [SerializeField] private float _speedPoints;
    protected override void GrantPowerUp(GameObject powerUpRecipient)
    {
        PlayerMovement playerMovement = powerUpRecipient.GetComponent<PlayerMovement>();
        if (playerMovement != null) 
        {
            playerMovement.ChangeMovementSpeed(_speedPoints);
        }
    }

    protected override void RevertPowerUp(GameObject powerUpRecipient)
    {
        PlayerMovement playerMovement = powerUpRecipient.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ChangeMovementSpeed(-_speedPoints);
        }
    }
}
