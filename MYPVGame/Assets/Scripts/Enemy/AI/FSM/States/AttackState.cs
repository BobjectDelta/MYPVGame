using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private ProjectileShooting _projectileShooting;
    public override void EnterState()
    {
        // Debug.Log("Entered: Attack");
    }

    public override void Execute()
    {
        npcMovement.ApproachPosition(enemyRadar.GetRadarTarget().position);
        _projectileShooting.Shoot();

        if (!enemyRadar.isTargetVisible)
            isComplete = true;
    }

    public override void ExitState()
    {
        // Debug.Log("Exited: Attack");
        npcMovement.StopMovement();
    }

    public void SetShootingComponent(ProjectileShooting shooting)
    {
        _projectileShooting = shooting;
    }
}
