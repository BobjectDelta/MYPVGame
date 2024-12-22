using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private ProjectileShooting _projectileShooting;
    private Vector3 _formationOffset = Vector3.zero;
    
    public override void EnterState()
    {
        // Calculate formation position offset based on position in formation
        if (formation.GetFormationSize() > 1)
        {
            _formationOffset = CalculateFormationOffset(formation.GetFormationSize());
        }
    }

    public override void Execute()
    {
        if (enemyRadar.GetRadarTarget() != null)
        {
            Vector3 targetPosition = enemyRadar.GetRadarTarget().position;
            
            // Apply formation offset when in formation
            if (formation.GetFormationSize() > 1)
            {
                Vector3 formationPosition = targetPosition + _formationOffset;
                npcMovement.ApproachPosition(formationPosition);
            }
            else
            {
                npcMovement.ApproachPosition(targetPosition);
            }

            // Only shoot if in range and has line of sight
            if (formation.IsTargetVisibleToFormation())
            {
                _projectileShooting.Shoot();
            }
        }

        if (!enemyRadar.isTargetVisible)
            isComplete = true;
    }

    public override void ExitState()
    {
        npcMovement.StopMovement();
    }

    public void SetShootingComponent(ProjectileShooting shooting)
    {
        _projectileShooting = shooting;
    }

    private Vector3 CalculateFormationOffset(int index)
    {
        // Create a V formation pattern
        float xOffset = (index % 2 == 0 ? 1 : -1) * (index + 1) * 1.5f;
        float yOffset = -index * 1.0f;
        return new Vector3(xOffset, yOffset, 0);
    }
}
