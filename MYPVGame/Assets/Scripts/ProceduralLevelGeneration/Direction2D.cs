using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirections = new List<Vector2Int>
    {
        new Vector2Int(0, 1), 
        new Vector2Int(1, 0), 
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0)
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirections[Random.Range(0, cardinalDirections.Count)];
    }
}
