using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;




public class MapGenerator : MonoBehaviour
{
    public List<Tile> prefabs;

    public GameObject ChunkGeneratorPrefab;

    public Vector2Int genSize;
    public Vector2 chunkSize;

    public GameObject activeChunk;
    public GameObject[,] Chunks;
    public Vector2Int MapDimensions;

    private ChunkGenerator chunkGenerator;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Init()
    {
        Chunks = new GameObject[MapDimensions.x, MapDimensions.y];
        chunkGenerator = Instantiate(ChunkGeneratorPrefab).GetComponent<ChunkGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateAdjacentChunks(Vector2Int ChunkPosition)
    {
        for (int dy = -1; dy <= 1; ++dy)
        {
            for (int dx = -1; dx <= 1; ++dx) 
            {
                Vector2Int curpos = new Vector2Int(ChunkPosition.x + dx, ChunkPosition.y + dy);
                if (Chunks[curpos.x,curpos.y] == null)
                {
                    chunkGenerator.GenerateChunk(curpos, 
                        Chunks[curpos.x - 1, curpos.y].GetComponent<Chunk>(), 
                        Chunks[curpos.x + 1, curpos.y].GetComponent<Chunk>(), 
                        Chunks[curpos.x, curpos.y - 1].GetComponent<Chunk>(), 
                        Chunks[curpos.x, curpos.y + 1].GetComponent<Chunk>()
                        );
                }
            }
        }
    }
    
}
