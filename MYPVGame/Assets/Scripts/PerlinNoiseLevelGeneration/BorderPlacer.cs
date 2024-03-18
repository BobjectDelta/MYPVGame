using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BorderPlacer
{
    public static void PlaceBorders(HashSet<Vector2Int> floorPositions, TilePainter tilePainter)
    {
        HashSet<Vector2Int> basicWallPositions = SearchForBorders(floorPositions, Direction2D.directions);

        foreach (Vector2Int position in basicWallPositions)
        {
            bool hasFloorUp = floorPositions.Contains(position + Vector2Int.up);
            bool hasFloorDown = floorPositions.Contains(position + Vector2Int.down);
            bool hasFloorLeft = floorPositions.Contains(position + Vector2Int.left);
            bool hasFloorRight = floorPositions.Contains(position + Vector2Int.right);

            tilePainter.PaintSingleWall(position, hasFloorUp, hasFloorDown, hasFloorLeft, hasFloorRight);
        }
    }

    private static HashSet<Vector2Int> SearchForBorders(HashSet<Vector2Int> floorPositions, List<Vector2Int> cardinalDirections)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        foreach (Vector2Int position in floorPositions)
        {
            foreach (Vector2Int direction in cardinalDirections)
            {
                Vector2Int neighbourPosition = position + direction;
                if (!floorPositions.Contains(neighbourPosition))               
                    wallPositions.Add(neighbourPosition);                
            }
        }
        return wallPositions;
    }
}
