using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    /*private float _patrolTimer = 0f;
    private Vector3 _patrolDestination;
    private const float PATROL_INTERVAL = 3f;*/
    // public List<Vector3> targetPositions = new() // get 
    // public List<Vector3> TargetPositions { get; } = new();

    public override void EnterState()
    {
        npcMovement.StopMovement();
        //SetNewPatrolDestination();
    }

    public override void Execute()
    {
        // Vector3 formationPosition = formation.GetFormationPosition(npcMovement.GetComponent<Enemy>());
        // Vector3 playerPos = enemyRadar.GetRadarTarget() != null ? enemyRadar.GetRadarTarget().position : Vector3.zero;
        // Vector3 directionFromPlayer = -playerPos.normalized;
        // float angle = Mathf.Atan2(directionFromPlayer.y, directionFromPlayer.x) * Mathf.Rad2Deg;
        // Quaternion formationRotation = Quaternion.Euler(0, 0, angle);
        // Vector3 rotatedFormationPosition = formationRotation * formationPosition;
        // Vector3 targetPosition = playerPos + rotatedFormationPosition;
        // TargetPositions.Add(targetPosition);
        // npcMovement.ApproachPosition(targetPosition, 0);

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

    /*private void SetNewPatrolDestination()
    {
        // Random point within reasonable radius
        float radius = 5f;
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        _patrolDestination = npcMovement.transform.position + new Vector3(randomPoint.x, randomPoint.y, 0);
    }*/
    
    // private Vector3 CalculateAverageEnemyPosition()
    // {
    //     Vector3 sum = Vector3.zero;
    //     int count = 0;
    //     foreach (Enemy enemy in formation.GetEnemies())
    //     {
    //         if (enemy != null && enemy.gameObject != npcMovement.gameObject)
    //         {
    //             sum += enemy.transform.position;
    //             count++;
    //         }
    //     }
    //     return count > 0 ? sum / count : npcMovement.transform.position;
    // }
}
