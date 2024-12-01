using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyRadar _enemyRadar;
    private FiniteStateMachine _fsm;

    private void Awake()
    {
        _enemyRadar = GetComponentInChildren<EnemyRadar>();
        _fsm = gameObject.AddComponent<FiniteStateMachine>();
    }

    private void FixedUpdate()
    {
        /*if (_enemyRadar.GetRadarTarget() != null)
        {
            if (_enemyRadar.isTargetVisible)
                if (_shootBehaviour != null)
                    _shootBehaviour.ExecuteAction(_enemyRadar);
            if (_chaseBehaviour != null)
                _chaseBehaviour.ExecuteAction(_enemyRadar);
        }*/
    }

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
}
