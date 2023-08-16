/* Script which defines the smallest map part - tile
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


//Define some different kinds of tiles
public enum TileType
{
    mud,
    grass,
    road,
    rocks
}

//Class mostly contains information about the single tile of square shape
public class Tile : MonoBehaviour
{
    //Define the kind of tile for using in checking connections at WFC in chunkGenerator
    [SerializeField] 
    private TileType tileType;
    public TileType type => tileType;

    //define the speed multiplier which applies to the player when he stands on this tile
    [SerializeField, Range(0, 2)]
    private float _speedMultiplier = 1f;
    public float SpeedMultiplier { get => _speedMultiplier;}

    //List of any objects located at this tile (such as obstacles, loot e.t.c.)
    public List<GameObject> objects;

    //Relative chance of spawn tile of this when generating map
    [SerializeField]
    private float _probability;
    public float probability => _probability;

    //Tile types which can be placed from every side of this tile
    public TileType[] availableConnectionsLeft;
    public TileType[] availableConnectionsRight;
    public TileType[] availableConnectionsTop;
    public TileType[] availableConnectionsBottom;
}

//TODO: change base class from monobehaviour to ScriptableObject