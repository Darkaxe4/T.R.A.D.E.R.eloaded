/* script defining behaviour of chunks - pieces of map which are being generated, displayed and hidden when the player moves throughout the map
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class of chunk - square or rectangular piece of map, containing multiple tiles
public class Chunk : MonoBehaviour
{
    //Size of this chunk in tiles
    public Vector2Int ChunkSize;

    //Array containing tile instances of this chunk
    public GameObject[,] tiles;

    //Global coordinates (x,y) of this chunk
    public Vector2Int ChunkPosition;

    //methods for getting border tiles of this chunk, used in ChunkGenerator to connect adjacent chunks together
    public Tile[] getLeftBorderTiles()
    {
        var result = new Tile[ChunkSize.y];
        for (int i = 0; i < ChunkSize.y; i++)
        {
            result[i] = tiles[0, i].GetComponent<Tile>();
        }
        return result;

    }
    public Tile[] getRightBorderTiles()
    {
        var result = new Tile[ChunkSize.y];
        for (int i = 0; i < ChunkSize.y; i++)
        {
            result[i] = tiles[ChunkSize.x - 1, i].GetComponent<Tile>();
        }
        return result;
    }
    public Tile[] getTopBorderTiles()
    {
        var result = new Tile[ChunkSize.x];
        for (int i = 0; i < ChunkSize.x; i++)
        {
            result[i] = tiles[i, ChunkSize.y - 1].GetComponent<Tile>();
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

    //method using for getting speed multiplier of tile at the local position relative to this chunk
    public float GetSpeedMultiplierAt(Vector2Int position)
    {
        return tiles[position.x, position.y].GetComponent<Tile>().SpeedMultiplier;
    }


}
