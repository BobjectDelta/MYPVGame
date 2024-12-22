using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private float _patrolTimer = 0f;
    private Vector3 _patrolDestination;
    private const float PATROL_INTERVAL = 3f;
    
    public override void EnterState()
    {
        SetNewPatrolDestination();
    }

    public override void Execute()
    {
        if (enemyRadar.isTargetVisible)
        {
            isComplete = true;
            return;
        }

        // // Handle patrol behavior when in formation
        // if (formation.GetFormationSize() > 1)
        // {
        //     // Follow formation leader's patrol
        //     var enemies = formation.GetEnemies();
        //     // Safe enumeration of enemies
        //     foreach (Enemy enemy in enemies)
        //     {
        //         // First enemy in formation is considered the leader
        //         if (enemy != null && enemy.gameObject != npcMovement.gameObject)
        //         {
        //             npcMovement.ApproachPosition(enemy.transform.position);
        //             break; // Only follow the first other enemy we find
        //         }
        //     }
        // }
        // else
        // {
        //     // Individual patrol behavior
        //     _patrolTimer += Time.deltaTime;
        //     if (_patrolTimer >= PATROL_INTERVAL)
        //     {
        //         SetNewPatrolDestination();
        //         _patrolTimer = 0f;
        //         npcMovement.ApproachPosition(_patrolDestination);
        //     }
        // }
    }

    public override void ExitState()
    {
        npcMovement.StopMovement();
    }

    private void SetNewPatrolDestination()
    {
        // Random point within reasonable radius
        float radius = 5f;
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        _patrolDestination = npcMovement.transform.position + new Vector3(randomPoint.x, randomPoint.y, 0);
    }
}
