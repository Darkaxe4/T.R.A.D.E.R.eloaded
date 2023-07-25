using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject playerGO;
    public GameObject generatorPrefab;
    private MapGenerator mapGenerator;

    public GameObject activeChunk;
    public GameObject[,] Chunks;
    public Vector2Int MapDimensions;
    private Vector2 mapSize;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame


    

    public Vector3 GetRelativeToActiveChunkPlayerPosition()
    {
        return playerGO.transform.position - new Vector3(activeChunk.GetComponent<Chunk>().ChunkPosition.x, activeChunk.GetComponent<Chunk>().ChunkPosition.y);
    }
}
