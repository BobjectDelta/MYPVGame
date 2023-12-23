using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstLevelGenerator : SimpleRandomWalkLevelGenerator
{
    [SerializeField] private int _minRoomWidth = 4;
    [SerializeField] private int _minRoomHeight = 4;
    [SerializeField] private int _levelWidth = 20;
    [SerializeField] private int _levelHeight = 20;
    [SerializeField] [Range(0, 10)] private int _offset = 1;
    //[SerializeField] private bool _randomWalkRooms = false;

    private List<HashSet<Vector2Int>> _individualRoomsFloors;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithm.BinarySpacePartitioning(new BoundsInt((Vector3Int)_startPosition,
            new Vector3Int(_levelWidth, _levelHeight, 0)), _minRoomWidth, _minRoomHeight);

        _individualRoomsFloors = new List<HashSet<Vector2Int>>();

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

       // if (_randomWalkRooms)       
            floor = CreateRoomsRandomly(roomsList);     
        //else
            //floor = CreateSimpleRooms(roomsList);

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        _agentPlacer.PlaceAgents(roomsList, _individualRoomsFloors);

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        _tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, _tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            _individualRoomsFloors.Add(new HashSet<Vector2Int>());
            foreach (var position in roomFloor)
            {
                if (position.x >= (roomBounds.xMin + _offset) && position.x <= (roomBounds.xMax - _offset) && position.y >= (roomBounds.yMin + _offset) && //
                    position.y <= (roomBounds.yMax - _offset))
                {
                    _individualRoomsFloors[i].Add(position);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while (position.y != destination.y ) 
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float length = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if (currentDistance < length) 
            {
                length = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = _offset; col < room.size.x - _offset; col++)
            {
                for (int row = _offset; row < room.size.y - _offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }

        return floor;
    }
}
