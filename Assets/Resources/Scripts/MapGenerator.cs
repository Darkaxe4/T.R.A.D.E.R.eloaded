using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;




public class MapGenerator : MonoBehaviour
{
    public GameObject ChunkGeneratorPrefab;

    public Vector2Int genSize;
    public Vector2Int chunkSize;

    public GameObject activeChunk;
    public Chunk[,] Chunks;
    public Vector2Int MapDimensions;

    private ChunkGenerator chunkGenerator;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Init()
    {
        Chunks = new Chunk[MapDimensions.x, MapDimensions.y];
        for (int i = 0; i < MapDimensions.x; i++)
        {
            for (int j = 0; j < MapDimensions.y; j++)
            {
                Chunks[i, j] = null;
            }
        }
        chunkGenerator = Instantiate(ChunkGeneratorPrefab).GetComponent<ChunkGenerator>();
        chunkSize = new Vector2Int(chunkGenerator.TileSize.x * chunkGenerator.ChunkSize.x, chunkGenerator.TileSize.y * chunkGenerator.ChunkSize.y);
        Chunks[MapDimensions.x / 2, MapDimensions.y / 2] = chunkGenerator.GenerateChunk(Vector2Int.zero, null, null, null, null);
        activeChunk = Chunks[MapDimensions.x / 2, MapDimensions.y / 2].gameObject;
        Debug.Log(new Vector2Int(MapDimensions.x / 2, MapDimensions.y / 2));
        Debug.Log(Chunks[MapDimensions.x / 2, MapDimensions.y / 2]);
        GenerateAdjacentChunks(new Vector2Int(MapDimensions.x / 2, MapDimensions.y / 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateAdjacentChunks(Vector2Int ChunkPosition)
    {
        var absChunkPosition = Chunks[ChunkPosition.x, ChunkPosition.y].ChunkPosition;
        if (ChunkPosition.x - 1 >= 0) 
        {
            Vector2Int lChunkPos = new Vector2Int(absChunkPosition.x - chunkSize.x, absChunkPosition.y);
            Chunks[ChunkPosition.x - 1, ChunkPosition.y] = chunkGenerator.GenerateChunk(lChunkPos,
                (ChunkPosition.x - 1 - 1 >= 0) ? Chunks[ChunkPosition.x - 1 - 1, ChunkPosition.y] : null,
                Chunks[ChunkPosition.x, ChunkPosition.y],
                (ChunkPosition.y - 1 >= 0) ? Chunks[ChunkPosition.x - 1, ChunkPosition.y - 1] : null,
                (ChunkPosition.y + 1 < MapDimensions.y) ? Chunks[ChunkPosition.x - 1, ChunkPosition.y + 1] : null
            ); 
        }


        if (ChunkPosition.x + 1 < MapDimensions.x)
        {
            Vector2Int rChunkPos = new Vector2Int(absChunkPosition.x + chunkSize.x, absChunkPosition.y);
            Chunks[ChunkPosition.x + 1, ChunkPosition.y] = chunkGenerator.GenerateChunk(rChunkPos,
                Chunks[ChunkPosition.x, ChunkPosition.y],
                (ChunkPosition.x + 1 + 1 < MapDimensions.x) ? Chunks[ChunkPosition.x + 1 + 1, ChunkPosition.y] : null,
                (ChunkPosition.y - 1 >= 0) ? Chunks[ChunkPosition.x + 1, ChunkPosition.y - 1] : null,
                (ChunkPosition.y + 1 < MapDimensions.y) ? Chunks[ChunkPosition.x + 1, ChunkPosition.y + 1] : null
                ); 
        }

        if (ChunkPosition.y - 1 >= 0)
        {
            Vector2Int downChunkPos = new Vector2Int(absChunkPosition.x, absChunkPosition.y - chunkSize.y);
            Chunks[ChunkPosition.x, ChunkPosition.y - 1] = chunkGenerator.GenerateChunk(downChunkPos,
                (ChunkPosition.x - 1 >= 0) ? Chunks[ChunkPosition.x - 1, ChunkPosition.y - 1] : null,
                (ChunkPosition.x + 1 < MapDimensions.x) ? Chunks[ChunkPosition.x + 1, ChunkPosition.y - 1] : null,
                (ChunkPosition.y - 1 - 1 >= 0) ? Chunks[ChunkPosition.x, ChunkPosition.y - 1 - 1] : null,
                Chunks[ChunkPosition.x, ChunkPosition.y]
                ); 
        }

        if (ChunkPosition.y + 1 < MapDimensions.y)
        {
            Vector2Int upChunkPos = new Vector2Int(absChunkPosition.x, absChunkPosition.y + chunkSize.y);
            Chunks[ChunkPosition.x, ChunkPosition.y + 1] = chunkGenerator.GenerateChunk(upChunkPos,
                (ChunkPosition.x - 1 >= 0) ? Chunks[ChunkPosition.x - 1, ChunkPosition.y + 1] : null,
                (ChunkPosition.x + 1 < MapDimensions.x) ? Chunks[ChunkPosition.x + 1, ChunkPosition.y + 1] : null,
                Chunks[ChunkPosition.x, ChunkPosition.y],
                (ChunkPosition.y + 1 < MapDimensions.y) ? Chunks[ChunkPosition.x, ChunkPosition.y + 1 + 1] : null
                ); 
        }

    }
    
}
