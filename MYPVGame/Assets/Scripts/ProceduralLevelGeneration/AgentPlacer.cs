using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField][Range(1, 10)] private int _maxEnemiesPerRoom;

    private int _playerRoomIndex;
    private int _offset = 2;
    private List<GameObject> _levelAgents = new List<GameObject>();
    private GameObject _enemyHolder;

    public void PlaceAgents(List<BoundsInt> rooms, List<HashSet<Vector2Int>> individualRoomsTiles)
    {
        _playerRoomIndex = Random.Range(0, rooms.Count);
        if (!_enemyHolder)
            _enemyHolder = new GameObject("EnemyHolder");

        for (int i = 0; i < rooms.Count; i++)
        {
            if (i == _playerRoomIndex)
            {
                GameObject player = Instantiate(_playerPrefab);
                player.transform.localPosition = rooms[i].center;
                _levelAgents.Add(player);
            }
            else
            {
                PlaceEnemies(rooms[i], individualRoomsTiles[i], Random.Range(1, _maxEnemiesPerRoom));
            }

        }
    }

    private void PlaceEnemies(BoundsInt room, HashSet<Vector2Int> roomTiles, int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            if (roomTiles.Count <= i)
                return;
            int xCenter = Mathf.RoundToInt((float)roomTiles.Average(x => x.x));
            int yCenter = Mathf.RoundToInt((float)roomTiles.Average(y => y.y));
            Vector2Int potentialEnemyPosition = new Vector2Int(Random.Range(xCenter - _offset, xCenter + _offset),
                Random.Range(yCenter - _offset, yCenter + _offset));
            if (roomTiles.Contains(potentialEnemyPosition))
            {
                GameObject enemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)]);
                enemy.transform.localPosition = (Vector2)potentialEnemyPosition + Vector2.one * 0.5f;
                enemy.transform.SetParent(_enemyHolder.transform);
                _levelAgents.Add(enemy);
            }
        }
    }

    public void Clear()
    {
        foreach (var agent in _levelAgents)        
            DestroyImmediate(agent);       
    }
}
