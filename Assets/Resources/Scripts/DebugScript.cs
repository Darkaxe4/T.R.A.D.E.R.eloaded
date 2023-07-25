using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public Vector2 TestField = new Vector2 (0, 0);
    public ChunkGenerator ChunkGenerator;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(TestField.ToString());
        ChunkGenerator.GenerateChunk(new Vector2Int(0, 0), null, null, null, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
