using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public string[] weaponNames;

    public Vector2Int mapSize;
    public int weaponCountToGen;
    public int armorCountToGen;
    public int miscellaneousCountToGen;

    public GameObject[] WeaponPeekPrefab;
    public GameObject[] WeaponHandlePrefab;
    public GameObject[] WeaponGardPrefab;
    public GameObject[] WeaponBladePrefab;

    public GameObject[] armorPrefab;
    public GameObject[] miscellaneousPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GenerateWeapon()
    {
        var result = new GameObject();
        result.AddComponent<Item>();
        ItemPart[] parts = new ItemPart[4];
        parts[0] = Instantiate(WeaponPeekPrefab[Random.Range(0, WeaponPeekPrefab.Length)].GetComponent<ItemPart>());
        parts[1] = Instantiate(WeaponHandlePrefab[Random.Range(0, WeaponHandlePrefab.Length)].GetComponent<ItemPart>());
        parts[2] = Instantiate(WeaponGardPrefab[Random.Range(0, WeaponGardPrefab.Length)].GetComponent<ItemPart>());
        parts[3] = Instantiate(WeaponBladePrefab[Random.Range(0, WeaponBladePrefab.Length)].GetComponent<ItemPart>());
        result.GetComponent<Item>().parts = parts;
        result.tag = "Collectable";
        
        result.GetComponent<Item>().Init(weaponNames[Random.Range(0, weaponNames.Length)]);

        result.GetComponent<Item>().parts[0].transform.localPosition = Vector3.zero;
        result.GetComponent<Item>().parts[1].transform.localPosition = result.GetComponent<Item>().parts[0].transform.position + result.GetComponent<Item>().parts[0].attachSlots[0] - result.GetComponent<Item>().parts[1].attachSlots[0];
        result.GetComponent<Item>().parts[2].transform.localPosition = result.GetComponent<Item>().parts[1].transform.position + result.GetComponent<Item>().parts[1].attachSlots[1] - result.GetComponent<Item>().parts[2].attachSlots[0];
        result.GetComponent<Item>().parts[3].transform.localPosition = result.GetComponent<Item>().parts[2].transform.position + result.GetComponent<Item>().parts[2].attachSlots[1] - result.GetComponent<Item>().parts[3].attachSlots[0];
        result.name = result.GetComponent<Item>().Name;
        return result;
    }
}
