using System;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public abstract void ExecuteAction(EnemyRadar enemyRadar);
}