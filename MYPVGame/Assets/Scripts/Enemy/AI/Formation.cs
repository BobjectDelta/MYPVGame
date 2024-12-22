using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    private static int _idCounter = -1;
    [SerializeField] private int id = ++_idCounter;
    private List<Enemy> _enemies { get; set; }
    // private Enemy _enemyToMerge = null;
    private List<Enemy> _enemiesToMerge = new();

    private void Awake()
    {
        _enemies = new List<Enemy> { GetComponent<Enemy>() };
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
        enemyToMerge.formation = this;
        _enemies.Add(enemyToMerge);
        
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
                UnityEditor.Handles.Label(enemy.transform.position + Vector3.right * 2, "Merged to f. : " + id);
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
}
