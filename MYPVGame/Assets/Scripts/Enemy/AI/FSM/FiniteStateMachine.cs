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
        Formation formation = gameObject.GetComponent<Formation>();
        BaseState newState;
        if (_enemyRadar.isTargetVisible) 
        {
            if (_enemyRadar.GetRadarEnemy() != null
                && formation.GetFormationSize() < 3
                && formation.GetFormationId() != _enemyRadar.GetRadarEnemy().GetComponent<Enemy>().formation.GetFormationId()
                )
                newState = _fleeState;
            else
                newState = _attackState;
                
        }
        else
        {
            newState = _idleState;
        }

        // _currentState.EnterState();
        if (newState.GetType() != _currentState.GetType()) {
            _currentState.ExitState();
            newState.EnterState();
            SetCurrentState(newState);
        }
    }
    
    public void SetFormationState(BaseState state, Formation formation)
    {
        foreach (Enemy enemy in formation.GetEnemies())
        {
            enemy.GetComponent<FiniteStateMachine>().SetCurrentState(state);
        }
    }

    private void SetCurrentState(BaseState state)
    {
        _previousState = _currentState;
        _currentState = state;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying && _currentState != null)
        {
            UnityEditor.Handles.Label(transform.position, _currentState.ToString());
            // foreach (Vector3 targetPos in ((IdleState)_idleState).TargetPositions)
            // {
            //     Gizmos.color = Color.green;
            //     Gizmos.DrawSphere(targetPos, 0.5f);
            // }
            // ((IdleState)_idleState).TargetPositions.Clear();
        }
#endif
    }
}
