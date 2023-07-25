using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Miscellaneous
}

public class Item : MonoBehaviour
{
    public ItemType Type;
    public int Cost;
    public string Name = "";
    public string Description;
    public ItemPart[] parts;

    public void Init(string name)
    {
        Name = "";
        foreach (var part in parts)
        {
            Name += part.nameModifier;
            part.SetParentTo(gameObject);
        }
        Name += name;
    }
}
