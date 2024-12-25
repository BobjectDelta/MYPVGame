using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    private static int _idCounter = -1;
    [SerializeField] private int id = ++_idCounter;
    private List<Enemy> _enemies { get; set; }
    private FormationRadar _formationRadar;
    private List<Enemy> _enemiesToMerge = new();
    private const int MAX_FORMATION_SIZE = 7;

    private void Awake()
    {
        _enemies = new List<Enemy> { GetComponent<Enemy>() };
        _formationRadar = gameObject.AddComponent<FormationRadar>();
    }
    
    // public Vector3 GetFormationPosition(Enemy enemy)
    // {
    //     // int index = _enemies.IndexOf(enemy);
    //     int index = enemy.formation.GetFormationId();
    //     Debug.Log(index);
    //     if (index == -1 || index >= MAX_FORMATION_SIZE)
    //     {
    //         return enemy.transform.position;
    //     }
    //
    //     // Leader is at index 0
    //     if (index == 0)
    //     {
    //         return Vector3.zero;
    //     }
    //
    //     // Calculate wing positions
    //     int side = (index % 2 == 0) ? 1 : -1;
    //     int row = (index + 1) / 2;
    //
    //     // Adjust these values to control V-shape spread
    //     float xSpread = side * row * 2f;  // Increased from 1.5f for wider spread
    //     float yOffset = -row * 1.5f;      // Increased from 1.0f for deeper V
    //
    //     return new Vector3(xSpread, yOffset, 0);
    // }
    
    public bool CanJoinFormation()
    {
        return _enemies.Count < MAX_FORMATION_SIZE;
    }
    
    public bool IsTargetVisibleToFormation()
    {
        return _formationRadar.IsTargetVisibleToFormation();
    }

    public void Merge()
    {
        List<Collider2D> allyColliders = gameObject.GetComponentInChildren<EnemyRadar>().GetVisibleEnemyColliders();
        // Debug.Log(allyColliders.Count);

        foreach (Collider2D collider in allyColliders) 
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if(enemy.formation.id < id)
            {
                foreach (Enemy enemyToMerge in enemy.formation._enemies)
                {
                    MergeEnemyToFormation(enemyToMerge);
                }
                // MergeEnemyToFormation(enemy); Because enemy is already in the enemy.formation._enemies
            }
        }
        /*if (formationToMerge != this)
        {
            foreach (Enemy enemy in formationToMerge._enemies)
            {
                enemy.formation = this;
            }
            _enemies.AddRange(formationToMerge._enemies);
        }*/
    }

    private void MergeEnemyToFormation(Enemy enemyToMerge)
    {
        if (!CanJoinFormation())
        {
            return;
        }
        
        enemyToMerge.formation = this;
        _enemies.Add(enemyToMerge);
        
        // Sync radar
        var enemyRadar = enemyToMerge.GetComponentInChildren<EnemyRadar>();
        if (enemyRadar != null)
        {
            enemyRadar.SetFormationRadar(_formationRadar);
        }
        
        _enemiesToMerge.Add(enemyToMerge);  // For debugging purposes
        // Debug.Log($"Enemy {enemyToMerge.name} merged to formation {id}");
    }

    public int GetFormationId()
    { 
        return id;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying && _enemiesToMerge.Count > 0)
        {
            foreach (Enemy enemy in _enemiesToMerge)
            {
                if (enemy != null)
                {
                    UnityEditor.Handles.Label(enemy.transform.position + Vector3.right * 2, "Merged to f. : " + id);
                }
            }
        }
#endif
    }

    public int GetFormationSize()
    {
        return _enemies.Count;
    }

    public IEnumerable GetEnemies()
    {
        return _enemies;
    }

    public void RemoveEnemyFromFormation(Enemy enemy)
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
        }
        if (_enemiesToMerge.Contains(enemy))
        {
            _enemiesToMerge.Remove(enemy);
        }
    }
}
