using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState
{
    private ProjectileShooting _projectileShooting;
    public override void EnterState()
    {
        Debug.Log("Entered: Attack");
    }

    public override void Execute()
    {
        npcMovement.ApproachPosition(enemyRadar.GetRadarPlayer().position);
        _projectileShooting.Shoot();

        if (!enemyRadar.isTargetVisible || (health.GetHealth() < health.GetMaxHealth() / 2 && enemyRadar.GetVisibleEnemyColliders().Count < 3))
            isComplete = true;
    }

    public override void ExitState()
    {
        Debug.Log("Exited: Attack");
        npcMovement.StopMovement();
    }

    public override void SetShootingComponent(ProjectileShooting shooting)
    {
        _projectileShooting = shooting;
    }
}
