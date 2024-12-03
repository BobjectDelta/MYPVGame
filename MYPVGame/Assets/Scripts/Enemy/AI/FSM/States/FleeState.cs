using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : BaseState
{
         
    public override void EnterState()
    {
        Debug.Log("Entered: Flee");
    }

    public override void Execute()
    {
        //npcMovement.FleeFromPosition(enemyRadar.GetRadarTarget().position);
        npcMovement.ApproachPosition(enemyRadar.GetRadarEnemy().position);
        //Debug.Log(enemyRadar.GetRadarEnemy().position);
        if (!enemyRadar.isTargetVisible)
            isComplete = true;
    }

    public override void ExitState()
    {
        Debug.Log("Exited: Flee");
        npcMovement.StopMovement();
    }
    
}
