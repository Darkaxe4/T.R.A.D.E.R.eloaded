using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPart : MonoBehaviour
{
    public float costMultiplier;
    public string nameModifier = "";
    public Vector3[] attachSlots;
    
    public void SetParentTo(GameObject parent)
    {
        transform.parent = parent.transform;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var slot in attachSlots)
        {
            Gizmos.DrawSphere(transform.position + slot, 0.1f);
        }
    }
}
