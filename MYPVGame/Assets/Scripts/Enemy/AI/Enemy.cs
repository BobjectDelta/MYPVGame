using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyRadar _enemyRadar;
    private FiniteStateMachine _fsm;
    public Formation formation { get; set; }

    private float _aggressionLevel;

    private void Awake()
    {
        _aggressionLevel = Random.Range(0f, 0.5f);
        formation = gameObject.AddComponent<Formation>();
        _enemyRadar = GetComponentInChildren<EnemyRadar>();
        _fsm = gameObject.GetComponent<FiniteStateMachine>();
    }

    public float GetAggressionLevel()
    {
        return _aggressionLevel;
    }

    public void ChangeAggressionLevel(float delta)
    {
        _aggressionLevel += delta;
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