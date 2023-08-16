/*Script containing class of the map generator using WFC alghorithm
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;



//Class using for managing, generating, saving and loading chunks as player going throughout the map
public class MapGenerator : MonoBehaviour
{
    //pointer to chunk generator prefab
    public GameObject ChunkGeneratorPrefab;

    //size of each chunk in unity metrics
    public Vector2Int chunkSize;

    //pointer to current active chunk where the player located
    public GameObject activeChunk;

    //array containing chunk objects of the map
    public Chunk[,] Chunks;

    //size of map to generate in chunks
    public Vector2Int MapDimensions;

    //pointer to chunk generator object
    private ChunkGenerator chunkGenerator;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    //debug method for testing map generator functionality
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

    //method using for generating adjacent chunks to chunk at the ChunkPosition
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

    public void SaveMap(string filename)
    {
        
    }
    
}
