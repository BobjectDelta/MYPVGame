using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractLevelGenerator : MonoBehaviour
{
    [SerializeField] protected TilemapVisualizer _tilemapVisualizer = null;
    [SerializeField] protected AgentPlacer _agentPlacer = null;
    [SerializeField] protected Vector2Int _startPosition = Vector2Int.zero;

    public void GenerateLevel()
    {
        _agentPlacer.Clear();
        _tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
