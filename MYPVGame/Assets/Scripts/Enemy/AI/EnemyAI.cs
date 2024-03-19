using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyBehaviour _shootBehaviour;
    [SerializeField] private EnemyBehaviour _chaseBehaviour;

    [SerializeField] private EnemyRadar _enemyRadar;

    private void Awake()
    {
        _enemyRadar = GetComponentInChildren<EnemyRadar>();

        _shootBehaviour = GetComponent<EnemyShootBehaviour>();
        _chaseBehaviour = GetComponent<EnemyChaseBehaviour>();
    }

    private void FixedUpdate()
    {
        if (_enemyRadar.GetRadarTarget() != null)
        {
            if (_enemyRadar.isTargetVisible)
                if (_shootBehaviour != null)
                    _shootBehaviour.ExecuteAction(_enemyRadar);
            if (_chaseBehaviour != null)
                _chaseBehaviour.ExecuteAction(_enemyRadar);
        }
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
