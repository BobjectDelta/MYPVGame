using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class NoiseFloorGenerator : MonoBehaviour
{
    [SerializeField] private int width = 32;
    [SerializeField] private int height = 32;

    [SerializeField] private float scale = 3;

    [SerializeField] private float offsetX = 10f;
    [SerializeField] private float offsetY = 10f;

    public HashSet<Vector2Int> GenerateNewLevelFloor()
    {
        HashSet<Vector2Int> levelFloor = new HashSet<Vector2Int>();

        do
        {
            offsetX = Random.Range(0f, 1000);
            offsetY = Random.Range(0f, 1000);

            levelFloor = GenerateFloor();
            Debug.Log(levelFloor.Count);
            levelFloor = GetLargestFloorRegion(levelFloor);
        } while (levelFloor.Count < (width * height) / 2);

        return levelFloor;
    }

    public HashSet<Vector2Int> GenerateFloor()
    {
        HashSet<Vector2Int> floorCoords = new HashSet<Vector2Int>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int floorTileCoords = GenerateFloorTile(x, y);
                if (floorTileCoords != Vector2Int.zero)
                    floorCoords.Add(floorTileCoords);
            }
        }

        return floorCoords;
    }

    public Vector2Int GenerateFloorTile(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        if (sample > 0.35 && sample < 0.8 && sample != 0.5)
            return new Vector2Int(x, y);
        else
            return Vector2Int.zero;
    }

    private HashSet<Vector2Int> GetLargestFloorRegion(HashSet<Vector2Int> floorCoords)
    {
        bool[,] visitedCoords = new bool[width, height];
        List<HashSet<Vector2Int>> floorRegions = new List<HashSet<Vector2Int>>();
        
        foreach (Vector2Int floorCoord in  floorCoords) 
        { 
            if (!visitedCoords[floorCoord.x, floorCoord.y])
            {
                HashSet<Vector2Int> floorRegion = FloodFill(floorCoords, visitedCoords);
                if (floorRegion.Count > 0)
                    floorRegions.Add(floorRegion);
            }
        }

        HashSet<Vector2Int> largestFloorRegion = new HashSet<Vector2Int>();
        foreach (HashSet<Vector2Int> floorRegion in floorRegions)
        {
            Debug.Log(floorRegion.Count());
            if (floorRegion.Count > largestFloorRegion.Count)
                largestFloorRegion = floorRegion;
        }

        return largestFloorRegion;
    }

    public HashSet<Vector2Int> FloodFill(HashSet<Vector2Int> floorCoords, bool[,] visited)
    {
        HashSet<Vector2Int> floorRegion = new HashSet<Vector2Int>();
        Queue<Vector2Int> coordsQueue = new Queue<Vector2Int>();
        
        coordsQueue.Enqueue(floorCoords.First());
        
        while (coordsQueue.Count != 0)
        {
            Vector2Int currentCoord = coordsQueue.Dequeue();
            int currentX = currentCoord.x;
            int currentY = currentCoord.y;

            if (floorCoords.Contains(currentCoord) && !visited[currentX, currentY])
            {
                visited[currentX, currentY] = true;
                floorRegion.Add(currentCoord);

                coordsQueue.Enqueue(new Vector2Int(currentX + 1, currentY));
                coordsQueue.Enqueue(new Vector2Int(currentX - 1, currentY));
                coordsQueue.Enqueue(new Vector2Int(currentX, currentY + 1));
                coordsQueue.Enqueue(new Vector2Int(currentX, currentY - 1));
            }
        }

        return floorRegion;
    }
}
