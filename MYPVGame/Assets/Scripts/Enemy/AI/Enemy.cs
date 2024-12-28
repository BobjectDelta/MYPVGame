using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyRadar _enemyRadar;
    private FiniteStateMachine _fsm;
    public Formation formation { get; set; }
    [SerializeField] private float _rabies;
    private int _escapeCount;

    private float _aggressionLevel;

    public float GetRabies()
    {
        return _rabies;
    }
    
    public void IncrementEscapeCount()
    {
        _escapeCount++;
        _rabies = CalculateRabies(_escapeCount);
    }
    
    private float CalculateRabies(int n, float upperBound = .7f)
    {
        return upperBound / (1 + Mathf.Exp(-n / 4f));
    }
    
    private void Awake()
    {
        _aggressionLevel = Random.Range(0f, 0.5f);
        formation = gameObject.AddComponent<Formation>();
        _enemyRadar = GetComponentInChildren<EnemyRadar>();
        _fsm = gameObject.GetComponent<FiniteStateMachine>();
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
