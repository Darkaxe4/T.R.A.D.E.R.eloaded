using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Vector2Int ChunkSize;

    public GameObject[,] tiles;

    public Vector2Int ChunkPosition;


    public Tile[] getLeftBorderTiles()
    {
        var result = new Tile[ChunkSize.y];
        for (int i = 0; i < ChunkSize.y; i++)
        {
            result[i] = tiles[0, i * ChunkSize.x].GetComponent<Tile>();
        }
        return result;

    }
    public Tile[] getRightBorderTiles()
    {
        var result = new Tile[ChunkSize.y];
        for (int i = 0; i < ChunkSize.y; i++)
        {
            result[i] = tiles[ChunkSize.x, i * ChunkSize.x].GetComponent<Tile>();
        }
        return result;
    }
    public Tile[] getTopBorderTiles()
    {
        var result = new Tile[ChunkSize.x];
        for (int i = 0; i < ChunkSize.x; i++)
        {
            result[i] = tiles[i, ChunkSize.y].GetComponent<Tile>();
        }
        return result;
    }
    public Tile[] getBottomBorderTiles()
    {
        var result = new Tile[ChunkSize.x];
        for (int i = 0; i < ChunkSize.x; i++)
        {
            result[i] = tiles[i, 0].GetComponent<Tile>();
        }
        return result;
    }

    public float GetSpeedMultiplierAt(Vector2Int position)
    {
        return tiles[position.x, position.y].GetComponent<Tile>().SpeedMultiplier;
    }


}
