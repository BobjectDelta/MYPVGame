using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    private static int _idCounter = -1;
    [SerializeField] private int id = ++_idCounter;
    private List<Enemy> _enemies { get; set; }

    private void Awake()
    {
        _enemies = new List<Enemy> { GetComponent<Enemy>() };
    }

    public void Merge(Formation formationToMerge)
    {
        List<Collider2D> allyColliders = gameObject.GetComponentInChildren<EnemyRadar>().GetVisibleEnemyColliders();

        foreach (Collider2D collider in allyColliders) 
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if(enemy.formation.id != id)
            {
                foreach (Enemy enemy1 in enemy.formation._enemies)
                {
                    enemy.formation = this;
                }
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

    public int GetFormationId()
    { 
        return id;
    }
}
