using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    [SerializeField] private BaseState _currentState;
    private BaseState _previousState;

    [SerializeField] private BaseState _idleState = new IdleState();
    [SerializeField] private FleeState _fleeState = new FleeState();
    private AttackState _attackState;
    [SerializeField] AttackStyle _attackStyle;

    private EnemyRadar _enemyRadar;
    private Health _health;

    public void Start()
    {
        _enemyRadar = gameObject.GetComponentInChildren<EnemyRadar>();
        _health = gameObject.GetComponent<Health>();
        NPCMovement npcMovement = gameObject.GetComponent<NPCMovement>();
        Formation formation = gameObject.GetComponent<Formation>();
        _idleState.Setup(_enemyRadar, npcMovement, _health, formation);
        _fleeState.Setup(_enemyRadar, npcMovement, _health, formation);
       
        if (_attackStyle == AttackStyle.Melee)
        {
            _attackState = new AttackState();
            npcMovement.SetApproachThreshhold(0f);
        }
        else
        {
            _attackState = new RangedAttackState();
            _attackState.SetShootingComponent(gameObject.GetComponentInChildren<ProjectileShooting>());
        }
        _attackState.Setup(_enemyRadar, npcMovement, _health, formation);
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
            if (_enemyRadar.GetVisibleEnemyColliders().Count < 3 && _health.GetHealth() < _health.GetMaxHealth() / 2)
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
           // UnityEditor.Handles.Label(transform.position + Vector3.down, _fleeState.GetFleeTime().ToString());
          //  UnityEditor.Handles.Label(transform.position + Vector3.down * 2, _fleeState.GetFleeTimer().ToString());

        }
#endif
    }

    public enum AttackStyle
    {
        Melee,
        Ranged
    }
}
