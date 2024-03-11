using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private TilePainter _tilePainter = null;
    [SerializeField] private EntitySpawner _entitySpawner = null;
    [SerializeField] private NoiseFloorGenerator _noiseFloorGenerator = null;

    private void Awake()
    {
        GenerateLevel();   
    }

    public void GenerateLevel()
    {
        _entitySpawner.ClearEntities();
        _tilePainter.Clear();

        CreateLevel();
    }

    private void CreateLevel()
    {
        HashSet<Vector2Int> levelFloor = _noiseFloorGenerator.GenerateNewLevelFloor();

        _tilePainter.PaintFloorTiles(levelFloor);
        BorderPlacer.PlaceBorders(levelFloor, _tilePainter);

        _entitySpawner.SpawnEntities(levelFloor);
    }
}
