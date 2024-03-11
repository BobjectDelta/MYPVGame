using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private GameObject[] _playerPrefabs;

    private List<GameObject> _levelEntities = new List<GameObject>();
    private GameObject _enemyHolder;

    public void SpawnEntities(HashSet<Vector2Int> levelFloor)
    {
        if (!_enemyHolder)
            _enemyHolder = new GameObject("EnemyHolder");
        
        HashSet<Vector2Int> spawnTiles = levelFloor;
        int enemyCount = Mathf.RoundToInt(Mathf.Sqrt(spawnTiles.Count));
        int enemyRadius = Mathf.RoundToInt(Mathf.Pow(enemyCount, 0.25f));

        SpawnPlayer(spawnTiles);
        SpawnEnemies(spawnTiles, enemyCount, enemyRadius);
    }

    private void SpawnPlayer(HashSet<Vector2Int> spawnTiles)
    {
        GameObject chosenPlayerPrefab = _playerPrefabs[PlayerPrefs.GetInt("selectedCharacterIndex")];
        Debug.Log(PlayerPrefs.GetInt("selectedCharacterIndex"));

        Vector3Int playerSpawnTile = (Vector3Int)spawnTiles.ElementAt(Random.Range(0, spawnTiles.Count));
        GameObject player = Instantiate(chosenPlayerPrefab);
        player.transform.localPosition = (Vector3)playerSpawnTile;
        _levelEntities.Add(player);
        ExcludeCoordsInRadius(spawnTiles, (Vector2Int)playerSpawnTile, 20);
    }

    private void SpawnEnemies(HashSet<Vector2Int> spawnTiles, int enemyCount, int enemyRadius)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3Int enemySpawnTile = (Vector3Int)spawnTiles.ElementAt(Random.Range(0, spawnTiles.Count));
            GameObject enemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)]);
            enemy.transform.localPosition = (Vector3)enemySpawnTile;
            enemy.transform.SetParent(_enemyHolder.transform);
            ExcludeCoordsInRadius(spawnTiles, (Vector2Int)enemySpawnTile, enemyRadius);
            _levelEntities.Add(enemy);
        }
    }

    private void ExcludeCoordsInRadius(HashSet<Vector2Int> coords, Vector2Int coord, int radius) 
    {
        List<Vector2Int> directions = Direction2D.cardinalDirections;

        for (int i = 0; i < radius; i++)
        {
            foreach (Vector2Int direction in directions)
                coords.Remove(coord + direction * (i + 1));
        }      
    }

    public void ClearEntities()
    {
        foreach (GameObject entity in _levelEntities)
            Destroy(entity);
    }
}
