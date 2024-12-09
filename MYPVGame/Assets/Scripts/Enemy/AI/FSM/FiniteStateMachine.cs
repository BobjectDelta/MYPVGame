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
    private AttackState _attackState = new AttackState();

    private EnemyRadar _enemyRadar;

    public void Start()
    {
        _enemyRadar = gameObject.GetComponentInChildren<EnemyRadar>();
        NPCMovement npcMovement = gameObject.GetComponent<NPCMovement>();
        Formation formation = gameObject.GetComponent<Formation>();
        _idleState.Setup(_enemyRadar, npcMovement, formation);
        _fleeState.Setup(_enemyRadar, npcMovement, formation);
        _attackState.Setup(_enemyRadar, npcMovement, formation);
        _attackState.SetShootingComponent(gameObject.GetComponentInChildren<ProjectileShooting>());
        _currentState = _idleState;
    }

    public void FixedUpdate() 
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
            if (_enemyRadar.GetRadarEnemy() != null)
                _currentState = _fleeState;
            else
                _currentState = _attackState;
        }
        else
        {
            _currentState = _idleState;
        }

        _currentState.EnterState();
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying && _currentState != null)
        {
            UnityEditor.Handles.Label(transform.position, _currentState.ToString());
        }
#endif
    }
}
