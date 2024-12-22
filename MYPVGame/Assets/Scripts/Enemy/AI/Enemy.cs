using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyRadar _enemyRadar;
    private FiniteStateMachine _fsm;
    public Formation formation { get; set; }

    private void Awake()
    {
        formation = gameObject.AddComponent<Formation>();
        _enemyRadar = GetComponentInChildren<EnemyRadar>();
        _fsm = gameObject.AddComponent<FiniteStateMachine>();
    }

    /*private void FixedUpdate()
    {
        if (_enemyRadar.GetRadarTarget() != null)
        {
            if (_enemyRadar.isTargetVisible)
                if (_shootBehaviour != null)
                    _shootBehaviour.ExecuteAction(_enemyRadar);
            if (_chaseBehaviour != null)
                _chaseBehaviour.ExecuteAction(_enemyRadar);
        }
    }*/

    private void DecrementEnemiesToDefeat()
    {
        if (GameManagement.gameManagerInstance != null)
            GameManagement.gameManagerInstance.DecrementEnemiesToDefeat();
    }

    public void DoBeforeDestruction()
    {
        DecrementEnemiesToDefeat();
        gameObject.GetComponent<DropPowerUpOnDestruction>().DropPowerUp();
    }
    
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying && formation != null)
        {
            UnityEditor.Handles.Label(transform.position + Vector3.right * 2, formation.GetFormationId().ToString());
        }
#endif
    }
}