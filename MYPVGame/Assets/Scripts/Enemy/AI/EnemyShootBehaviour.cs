using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootBehaviour : EnemyBehaviour
{
    [SerializeField] private ProjectileShooting _projectileShooting;

    private void Awake()
    {
        _projectileShooting = GetComponentInChildren<ProjectileShooting>();
    }

    public override void ExecuteAction(EnemyRadar enemyRadar)
    {
        _projectileShooting.Shoot();
    }
}
