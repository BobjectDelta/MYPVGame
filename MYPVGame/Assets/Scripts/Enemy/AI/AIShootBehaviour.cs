using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootBehaviour : AIBehaviour
{
    [SerializeField] private ProjectileShooting _projectileShooting;

    private void Awake()
    {
        _projectileShooting = GetComponentInChildren<ProjectileShooting>();
    }
    public override void PerformAction(AIDetector detector)
    {
        _projectileShooting.Shoot();
    }
}
