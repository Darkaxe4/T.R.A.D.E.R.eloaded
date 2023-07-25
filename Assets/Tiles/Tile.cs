using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum TileType
{
    mud,
    grass,
    road,
    rocks
}

public class Tile : MonoBehaviour
{
    [SerializeField] 
    private TileType tileType;
    public TileType type => tileType;

    [SerializeField, Range(0, 2)]
    private float _speedMultiplier = 1f;

    public float SpeedMultiplier { get => _speedMultiplier;}

    public List<GameObject> objects;

    [SerializeField]
    private float _probability;

    public float probability => _probability;

    public TileType[] availableConnectionsLeft;
    public TileType[] availableConnectionsRight;
    public TileType[] availableConnectionsTop;
    public TileType[] availableConnectionsBottom;

}
