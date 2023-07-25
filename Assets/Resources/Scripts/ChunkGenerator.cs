using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkGenerator : MonoBehaviour
{
    public Vector2 TileSize;
    public Vector2Int ChunkSize;

    public List<Tile> TilePrefabs;

    public Chunk Chunk;

    private List<int>[,] PossibleTilesIDS;


    public GameObject PlaceTile(Tile tileToPlace, Vector2Int pos)
    {
        PossibleTilesIDS[pos.x, pos.y] = null;

        var result = Instantiate(tileToPlace,
            new Vector3(Chunk.ChunkPosition.x, Chunk.ChunkPosition.y) + new Vector3(pos.x * TileSize.x, pos.y * TileSize.y),
            tileToPlace.transform.rotation).gameObject;
        result.gameObject.transform.parent = Chunk.transform;

        return result;
    }

    public Tile ChooseTile(Vector2Int position)
    {
        float totalProbability = 0f;
        var probs = new List<float>();
        foreach (var tile in PossibleTilesIDS[position.x, position.y])
        {
            totalProbability += TilePrefabs[tile].probability;
            probs.Add(TilePrefabs[tile].probability);
        }
        var val = Random.Range(0, totalProbability);
        int ptr = 0;
        while (val - probs[ptr] > 0)
        {
            val -= probs[ptr++];
        }
        return TilePrefabs[PossibleTilesIDS[position.x, position.y][ptr]];
    }

    private bool CanBePlacedTogether(Tile existing, Tile placing, Vector2Int direction)
    {
        if (existing == null) return true;
        if (direction == Vector2Int.left)
        {
            foreach (var existingConnection in existing.availableConnectionsLeft)
            {
                foreach (var placingConnection in placing.availableConnectionsRight)
                {
                    if ((existingConnection == placingConnection)) return true;
                }
            }
        }
        if (direction == Vector2Int.right)
        {
            foreach (var existingConnection in existing.availableConnectionsRight)
            {
                foreach (var placingConnection in placing.availableConnectionsLeft)
                {
                    if ((existingConnection == placingConnection)) return true;
                }
            }
        }
        if (direction == Vector2Int.up)
        {
            foreach (var existingConnection in existing.availableConnectionsTop)
            {
                foreach (var placingConnection in placing.availableConnectionsBottom)
                {
                    if ((existingConnection == placingConnection)) return true;
                }
            }
        }
        if (direction == Vector2Int.down)
        {
            foreach (var existingConnection in existing.availableConnectionsBottom)
            {
                foreach (var placingConnection in placing.availableConnectionsTop)
                {
                    if ((existingConnection == placingConnection)) return true;
                }
            }
        }
        return false;
    }

    private void RecalculatePossible(Vector2Int position)
    {
        if (position.x - 1 >= 0)
        {
            if (PossibleTilesIDS[position.x - 1, position.y] != null)
                foreach (var tile in PossibleTilesIDS[position.x - 1, position.y].ToList())
                {
                    if (!CanBePlacedTogether(Chunk.tiles[position.x, position.y].GetComponent<Tile>(), TilePrefabs[tile], Vector2Int.left)) PossibleTilesIDS[position.x - 1, position.y].Remove(tile);
                }
        }
        if (position.x + 1 < ChunkSize.x)
        {
            if (PossibleTilesIDS[position.x + 1, position.y] != null)
                foreach (var tile in PossibleTilesIDS[position.x + 1, position.y].ToList())
                {
                    if (!CanBePlacedTogether(Chunk.tiles[position.x, position.y].GetComponent<Tile>(), TilePrefabs[tile], Vector2Int.right)) PossibleTilesIDS[position.x + 1, position.y].Remove(tile);
                }
        }
        if (position.y - 1 >= 0)
        {
            if (PossibleTilesIDS[position.x, position.y - 1] != null)
                foreach (var tile in PossibleTilesIDS[position.x, position.y - 1].ToList())
                {
                    if (!CanBePlacedTogether(Chunk.tiles[position.x, position.y].GetComponent<Tile>(), TilePrefabs[tile], Vector2Int.down)) PossibleTilesIDS[position.x, position.y - 1].Remove(tile);
                }
        }
        if (position.y + 1 < ChunkSize.y)
        {
            if (PossibleTilesIDS[position.x, position.y + 1] != null)
                foreach (var tile in PossibleTilesIDS[position.x, position.y + 1].ToList())
                {
                    if (!CanBePlacedTogether(Chunk.tiles[position.x, position.y].GetComponent<Tile>(), TilePrefabs[tile], Vector2Int.up)) PossibleTilesIDS[position.x, position.y + 1].Remove(tile);
                }
        }

    }

    private void AddAdjacentTilesToQueue(ref Queue<Vector2Int> queue, Vector2Int currentTile)
    {
        if (currentTile.x - 1 >= 0)
        {
            if ((PossibleTilesIDS[currentTile.x - 1, currentTile.y] != null) && !(queue.Contains(currentTile + Vector2Int.left)))
            {
                queue.Enqueue(currentTile + Vector2Int.left);
            }

        }
        if (currentTile.x + 1 < ChunkSize.x)
        {
            if ((PossibleTilesIDS[currentTile.x + 1, currentTile.y] != null) && !(queue.Contains(currentTile + Vector2Int.right)))
            {
                queue.Enqueue(currentTile + Vector2Int.right);
            }

        }
        if (currentTile.y - 1 >= 0)
        {
            if ((PossibleTilesIDS[currentTile.x, currentTile.y - 1] != null) && !(queue.Contains(currentTile + Vector2Int.down)))
            {
                queue.Enqueue(currentTile + Vector2Int.down);
            }

        }
        if (currentTile.y + 1 < ChunkSize.y)
        {
            if ((PossibleTilesIDS[currentTile.x, currentTile.y + 1] != null) && !(queue.Contains(currentTile + Vector2Int.up)))
            {
                queue.Enqueue(currentTile + Vector2Int.up);
            }

        }
    }

    private void RecalculateBorders(Tile[] leftBorder, Tile[] rightBorder, Tile[] bottomBorder, Tile[] topBorder)
    {
        
        for (int i = 0; i < ChunkSize.y; ++i)
        {
            if (leftBorder != null)
            foreach (var tile in PossibleTilesIDS[0, i].ToArray())
            {
                if (!CanBePlacedTogether(leftBorder[i], TilePrefabs[tile], Vector2Int.right)) { PossibleTilesIDS[0, i].Remove(tile); }
            }
            if (rightBorder != null)
            foreach (var tile in PossibleTilesIDS[ChunkSize.x - 1, i].ToArray())
            {
                if (!CanBePlacedTogether(rightBorder[i], TilePrefabs[tile], Vector2Int.left)) { PossibleTilesIDS[ChunkSize.x - 1, i].Remove(tile); }
            }
        }
        for (int i = 0; i < ChunkSize.x; ++i)
        {
            if (bottomBorder != null)
            foreach (var tile in PossibleTilesIDS[i, 0].ToArray())
            {
                if (!CanBePlacedTogether(bottomBorder[i], TilePrefabs[tile], Vector2Int.up)) { PossibleTilesIDS[i, 0].Remove(tile); }
            }
            if (topBorder != null)
            foreach (var tile in PossibleTilesIDS[i, ChunkSize.y - 1].ToArray())
            {
                if (!CanBePlacedTogether(topBorder[i], TilePrefabs[tile], Vector2Int.down)) { PossibleTilesIDS[i, ChunkSize.y - 1].Remove(tile); }
            }
        }
    }

    public void GenerateChunk(Vector2Int position, Chunk leftChunk, Chunk rightChunk, Chunk bottomChunk, Chunk topChunk)
    {
        var Temp = new GameObject();
        Temp.AddComponent<Chunk>();
        Chunk = Temp.GetComponent<Chunk>();
        Chunk.ChunkPosition = position;
        Temp.name = position.ToString();
        Chunk.ChunkSize = ChunkSize;
        Chunk.tiles = new GameObject[ChunkSize.x, ChunkSize.y];

        PossibleTilesIDS = new List<int>[ChunkSize.x, ChunkSize.y];
        for (int i = 0; i < ChunkSize.x; i++)
            for (int j = 0; j < ChunkSize.y; j++)
            {
                PossibleTilesIDS[i, j] = new List<int>();
                for (int k = 0; k < TilePrefabs.Count; ++k)
                {
                    PossibleTilesIDS[i, j].Add(k);
                }
            }

        Tile[] leftBorder = null;
        if (leftChunk != null) { leftBorder = leftChunk.getRightBorderTiles(); }
        Tile[] rightBorder = null;
        if (rightChunk != null) {  rightBorder = rightChunk.getLeftBorderTiles();}
        Tile[] bottomBorder = null;
        if (bottomChunk != null) {  bottomBorder = bottomChunk.getTopBorderTiles(); }
        Tile[] topBorder = null;
        if (topChunk != null) {  topBorder = topChunk.getBottomBorderTiles();}
        
        RecalculateBorders(leftBorder, rightBorder, bottomBorder, topBorder);

        var startTile = new Vector2Int(ChunkSize.x / 2, ChunkSize.y / 2);
        Chunk.tiles[startTile.x, startTile.y] = PlaceTile(ChooseTile(startTile), startTile);
        Chunk.tiles[startTile.x, startTile.y].gameObject.name = startTile.ToString();
        RecalculatePossible(startTile);

        Queue<Vector2Int> TilesToPlace = new Queue<Vector2Int>();
        AddAdjacentTilesToQueue(ref TilesToPlace, startTile);

        while (TilesToPlace.Count > 0)
        {
            var currentTile = TilesToPlace.Dequeue();
            Debug.Log(currentTile);
            Chunk.tiles[currentTile.x, currentTile.y] = PlaceTile(ChooseTile(currentTile), currentTile);
            Chunk.tiles[currentTile.x, currentTile.y].gameObject.name = currentTile.ToString();
            RecalculatePossible(currentTile);
            AddAdjacentTilesToQueue(ref TilesToPlace, currentTile);
        }
    }
}