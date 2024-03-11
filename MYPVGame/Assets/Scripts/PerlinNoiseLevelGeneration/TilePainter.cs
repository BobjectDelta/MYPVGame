using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField] private Tilemap _floorTilemap;
    [SerializeField] private Tilemap _wallTilemap;

    [SerializeField] private TileBase _floorTile;

    [SerializeField] private TileBase _wallBase;
    [SerializeField] private TileBase _wallDown;
    [SerializeField] private TileBase _wallUp;
    [SerializeField] private TileBase _wallLeft;
    [SerializeField] private TileBase _wallRight;

    public void PaintFloorTiles(HashSet<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, _floorTilemap, _floorTile);
    }

    private void PaintTiles(HashSet<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (Vector2Int position in positions)
            PaintSingleTile(tilemap, tile, position);
    }

    public void PaintSingleWall(Vector2Int position, bool hasFloorUp, bool hasFloorDown, bool hasFloorLeft, bool hasFloorRight)
    {
        if (hasFloorUp && hasFloorDown && hasFloorLeft && hasFloorRight)
            PaintSingleTile(_wallTilemap, _wallBase, position);
        else if (hasFloorUp)
            PaintSingleTile(_wallTilemap, _wallDown, position);
        else if (hasFloorDown)
            PaintSingleTile(_wallTilemap, _wallUp, position);
        else if (hasFloorLeft)
            PaintSingleTile(_wallTilemap, _wallRight, position);
        else if (hasFloorRight)
            PaintSingleTile(_wallTilemap, _wallLeft, position);
    }
    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        Vector3Int tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        _floorTilemap.ClearAllTiles();
        _wallTilemap.ClearAllTiles();
    }
}
