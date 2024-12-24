using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : BaseState
{
    private float _fleeTimer = 0f;
    private const float FLEE_TIMEOUT = 3f; // Maximum time to spend fleeing

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
        formation.Merge();
        npcMovement.StopMovement();
    }

    /*public override void EnterState()
    {
        _fleeTimer = 0f;
    }

    public override void Execute()
    {
        _fleeTimer += Time.deltaTime;

        if (!enemyRadar.isTargetVisible || 
            enemyRadar.GetRadarEnemy() == null || 
            _fleeTimer >= FLEE_TIMEOUT)
        {
            isComplete = true;
            return;
        }

        // If we're already in a large enough formation, switch to attack
        if (formation.GetFormationSize() >= 3)
        {
            isComplete = true;
            return;
        }

        // Move towards potential ally
        if (enemyRadar.GetRadarEnemy() != null)
        {
            Vector3 allyPosition = enemyRadar.GetRadarEnemy().position;
            npcMovement.ApproachPosition(allyPosition);
        }
    }

    public override void ExitState()
    {
        // Only attempt merge if we haven't reached timeout
        *//*if (_fleeTimer < FLEE_TIMEOUT)
        {
            formation.Merge();
        }*//*
        formation.Merge();
        npcMovement.StopMovement();
    }*/
}

