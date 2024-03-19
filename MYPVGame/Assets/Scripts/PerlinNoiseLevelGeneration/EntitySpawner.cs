using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        Debug.Log(spawnTiles.Count);

        SpawnPlayer(spawnTiles);
                  
        Debug.Log(spawnTiles.Count);
        SpawnEnemies(spawnTiles, enemyCount, enemyRadius);
    }

    private void SpawnPlayer(HashSet<Vector2Int> spawnTiles)
    {
        GameObject chosenPlayerPrefab = _playerPrefabs[PlayerPrefs.GetInt("selectedCharacterIndex")];
        Debug.Log(PlayerPrefs.GetInt("selectedCharacterIndex"));

        Vector3Int playerSpawnTile = (Vector3Int)spawnTiles.ElementAt(Random.Range(0, spawnTiles.Count));
        GameObject player = Instantiate(chosenPlayerPrefab);
        player.transform.localPosition = (Vector3)playerSpawnTile + new Vector3(1, 1) * 0.5f;
        _levelEntities.Add(player);
        ExcludeCoordsInRadius(spawnTiles, (Vector2Int)playerSpawnTile, 5);
    }

    private void SpawnEnemies(HashSet<Vector2Int> spawnTiles, int enemyCount, int enemyRadius)
    {
        for (int i = 0; i < enemyCount && spawnTiles.Count > 0; i++)
        {
            Vector3Int enemySpawnTile = (Vector3Int)spawnTiles.ElementAt(Random.Range(0, spawnTiles.Count));
            GameObject enemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)]);
            enemy.transform.localPosition = (Vector3)enemySpawnTile + new Vector3(1, 1) * 0.5f;
            enemy.transform.SetParent(_enemyHolder.transform);
            ExcludeCoordsInRadius(spawnTiles, (Vector2Int)enemySpawnTile, enemyRadius);
            _levelEntities.Add(enemy);
        }
    }

    private void ExcludeCoordsInRadius(HashSet<Vector2Int> coords, Vector2Int coord, int radius) 
    {
        coords.Remove(coord);

        Vector2Int currentPosition = coord + new Vector2Int(-1, 1) * radius;
        int boundLength = radius * 2 + 1;

        for (int i = 0; i < boundLength; i++)
        {
            for (int j = 0; j < boundLength; j++)
            {
                coords.Remove(currentPosition);
                currentPosition.x = ++currentPosition.x;
            }
            currentPosition.x = currentPosition.x - boundLength;
            currentPosition.y = --currentPosition.y;
        }    
    }

    public void ClearEntities()
    {
        foreach (GameObject entity in _levelEntities)
            Destroy(entity);
    }
}
