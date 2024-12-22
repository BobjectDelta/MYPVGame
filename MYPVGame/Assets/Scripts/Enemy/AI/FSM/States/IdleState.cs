using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{

    public override void EnterState()
    {
        // Debug.Log("Entered: Idle");
    }

    public override void Execute()
    {
        if (enemyRadar.isTargetVisible)
            isComplete = true;
    }

    public override void ExitState()
    {
        // Debug.Log("Exited: Idle");
    }
}
