using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    [SerializeField] private BaseState _currentState;
    private BaseState _previousState;

    [SerializeField] private BaseState _idleState = new IdleState();
    [SerializeField] private FleeState _fleeState = new();
    private AttackState _attackState = new();
    private Enemy _enemy;
    
    private float _lastRabiesCheckTime;
    private float _rabiesCheckMemoryDuration = 1.5f;
    private bool _shouldFlee;

    private EnemyRadar _enemyRadar;
    private Health _health;

    public void Start()
    {
        _enemyRadar = gameObject.GetComponentInChildren<EnemyRadar>();
        _health = gameObject.GetComponent<Health>();
        NPCMovement npcMovement = gameObject.GetComponent<NPCMovement>();
        Formation formation = gameObject.GetComponent<Formation>();
        _enemy = gameObject.GetComponent<Enemy>();
        _idleState.Setup(_enemyRadar, npcMovement, _health, formation, _enemy);
        _fleeState.Setup(_enemyRadar, npcMovement, _health, formation, _enemy);
        _attackState.Setup(_enemyRadar, npcMovement, _health, formation, _enemy);
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

    private void SelectState()
    {
        if (_enemyRadar.isTargetVisible)
        {
            bool shouldCheckRabies = Time.time - _lastRabiesCheckTime > _rabiesCheckMemoryDuration;

            if (_enemyRadar.GetVisibleEnemyColliders().Count < 3 && _health.GetHealth() < _health.GetMaxHealth() / 2)
            {
                if (shouldCheckRabies)
                {
                    _shouldFlee = Random.value > _enemy.GetRabies();
                    _lastRabiesCheckTime = Time.time;
                }

                if (_shouldFlee)
                {
                    _currentState = _fleeState;
                }
                else
                {
                    _currentState = _attackState;
                }
            }
            else
            {
                _currentState = _attackState;
            }
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
}
