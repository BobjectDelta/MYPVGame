using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{

    public override void EnterState()
    {
        Debug.Log("Entered: Attack");
    }

    public override void Execute()
    {
        Debug.Log("Executing: Attack");
        npcMovement.ApproachPosition(enemyRadar.GetRadarTarget().position);
        if (!enemyRadar.isTargetVisible)
            isComplete = true;
    }

    public override void ExitState()
    {
        Debug.Log("Exited: Attack");
    }

}
