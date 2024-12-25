using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : BaseState
{
    /*private float _fleeTimer = 10f;
    private float _fleeTime = 0f;*/
         
    public override void EnterState()
    {
        Debug.Log("Entered: Flee");
        enemy.IncrementEscapeCount();
        /*_fleeTime = 0f;
        _fleeTimer = 10f / npcMovement.GetMovementSpeed();*/
    }

    public override void Execute()
    {
        if (!enemyRadar.GetRadarPlayer() /*&& _fleeTime >= _fleeTimer*/)
        {
            isComplete = true;
            return;
        }

        if (enemyRadar.isAllyVisible && enemyRadar.GetVisibleEnemyColliders().Count > 2)
            npcMovement.ApproachPosition(enemyRadar.GetRadarEnemy().position);
        else
            npcMovement.FleeFromPosition(enemyRadar.GetLastPlayerPosition());

        //_fleeTime += Time.deltaTime;
    }

    public override void ExitState()
    {
        Debug.Log("Exited: Flee");
        //formation.Merge(enemyRadar.GetRadarEnemy().gameObject.GetComponent<Formation>());
        npcMovement.StopMovement();
    }

    /*public float GetFleeTime()
    { 
        return _fleeTime;
    }
    public float GetFleeTimer()
    {
        return _fleeTimer;
    }*/
}
