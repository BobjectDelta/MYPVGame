using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private ProjectileShooting _projectileShooting;
    private Vector3 _formationOffset = Vector3.zero;
    // private List<Vector3> _targetPositions = new List<Vector3>();

    public override void EnterState()
    {
        Debug.Log("Entered: Attack");
    }

    public override void Execute() {
        Transform target = enemyRadar.GetRadarTarget();
    
        if (!enemyRadar.isTargetVisible || target == null) {
            isComplete = true;
            return;
        }
    
        // Calculate formation position
        // Vector3 formationPosition = formation.GetFormationPosition(npcMovement.GetComponent<Enemy>());
        // // _targetPositions.Add(formationPosition);
        // Vector3 directionToTarget = target.position - npcMovement.transform.position;
        // float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        // Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);
        // Vector3 rotatedFormationPosition = targetRotation * formationPosition;
        // Vector3 targetPosition = target.position + rotatedFormationPosition;
        
        // _targetPositions.Add(targetPosition);
        
        // Move to position
        npcMovement.ApproachPosition(target.position);
    
        // Shoot if we have line of sight
        if (formation.IsTargetVisibleToFormation() && _projectileShooting != null) {
            _projectileShooting.Shoot();
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exited: Attack");
        npcMovement.StopMovement();
    }

    public void SetShootingComponent(ProjectileShooting shooting)
    {
        _projectileShooting = shooting;
    }
    
    // public List<Vector3> GetTargetPositions()
    // {
    //     return _targetPositions;
    // }

    /*public override void EnterState()
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
    }*/
}
