using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 10f;
    public float terrainSpeedMultiplier = 1f;

    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;
        transform.Translate(direction * speed * terrainSpeedMultiplier * Time.deltaTime, Space.World);

        // Rotate towards moving direction
        float rotation = transform.rotation.eulerAngles.z;
        if(horizontalInput == 0 && verticalInput != 0)
        {
            rotation = 90 - verticalInput * 90;
        }
        else if(horizontalInput != 0)
        {
            rotation = (-90 + verticalInput * 45) * horizontalInput;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    public float GetCurrentSpeed()
    {
        return speed * terrainSpeedMultiplier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            InitiateTrade(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Item"))
        {
            PickUpItem(collision.gameObject);
        }
    }

    public void InitiateTrade(GameObject npc)
    {
        Debug.Log("Started Trade with " + npc.name);
    }

    public void PickUpItem(GameObject item)
    {
        Debug.Log("Picked up" + item.name);
    }
}
