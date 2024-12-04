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
        if (formationToMerge != this)
        {
            foreach (Enemy enemy in formationToMerge._enemies)
            {
                enemy.formation = this;
            }
            _enemies.AddRange(formationToMerge._enemies);
        }
    }
}
