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
        if (!enemyRadar.isTargetVisible || enemyRadar.GetRadarEnemy() == null)
        {
            isComplete = true;
            return;
        }
        //npcMovement.FleeFromPosition(enemyRadar.GetRadarTarget().position);
        npcMovement.ApproachPosition(enemyRadar.GetRadarEnemy().position);
        //Debug.Log(enemyRadar.GetRadarEnemy().position);
    }

    public override void ExitState()
    {
        Debug.Log("Exited: Flee");
        //formation.Merge(enemyRadar.GetRadarEnemy().gameObject.GetComponent<Formation>());
        npcMovement.StopMovement();
    }
    
}
