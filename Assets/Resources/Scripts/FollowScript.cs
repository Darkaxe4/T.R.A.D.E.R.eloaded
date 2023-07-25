using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public GameObject objectToFollow;
    public float followingDistance = 3f;
    public float cameraSpeed = 25f;
    // Start is called before the first frame update
    void Start()
    {
        objectToFollow = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        cameraSpeed = objectToFollow.GetComponent<PlayerController>().GetCurrentSpeed();
        if (((Vector2)objectToFollow.GetComponent<Transform>().position - (Vector2)transform.position).magnitude > followingDistance) 
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y, transform.position.z), Time.deltaTime * cameraSpeed);
        }
    }
}
