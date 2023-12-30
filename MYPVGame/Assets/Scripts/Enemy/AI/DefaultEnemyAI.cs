using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultEnemyAI : MonoBehaviour
{
    [SerializeField] private AIBehaviour _shootBehaviour;
    [SerializeField] private AIBehaviour _chaseBehaviour;

    [SerializeField] private AIDetector _detector;

    private void Awake()
    {
        _detector = GetComponentInChildren<AIDetector>();
        _shootBehaviour = GetComponent<AIShootBehaviour>();
        _chaseBehaviour = GetComponent<AIChaseBehaviour>();
    }

    private void FixedUpdate()
    {
        if (_detector.Target != null)
        {
            if (_detector.isTargetVisible)
                if (_shootBehaviour != null)
                    _shootBehaviour.PerformAction(_detector);
            if (_chaseBehaviour != null)
                _chaseBehaviour.PerformAction(_detector);
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
