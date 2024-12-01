using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    [SerializeField] private BaseState _currentState;
    private BaseState _previousState;

    [SerializeField] private BaseState _idleState = new IdleState();
    [SerializeField] private BaseState _fleeState = new FleeState();
    private BaseState _attackState = new AttackState();

    private EnemyRadar _enemyRadar;

    public void Start()
    {
        _enemyRadar = gameObject.GetComponentInChildren<EnemyRadar>();
        NPCMovement npcMovement = gameObject.GetComponent<NPCMovement>();
        _idleState.Setup(_enemyRadar, npcMovement);
        _fleeState.Setup(_enemyRadar, npcMovement);
        _attackState.Setup(_enemyRadar, npcMovement);
        _currentState = _idleState;
    }

    public void Update() 
    {
        if (_currentState.isComplete)
        {
            _currentState.ExitState();
            SelectState();
        }

        _currentState.Execute();
    }

    public void SelectState() 
    {
        if (_enemyRadar.isTargetVisible) 
        {
            //if (Random.value >= 0.5f)
                _currentState = _fleeState;
            /*else
                _currentState = _attackState;*/
        }
        else
        {
            _currentState = _idleState;
        }

        _currentState.EnterState();
    }
}
