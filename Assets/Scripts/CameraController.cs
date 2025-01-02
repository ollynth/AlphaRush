using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's transform
    [SerializeField] private Vector3 yOffset; // Offset from the player
    [SerializeField] private float FollowSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned in the CameraController.");
        }
    }

    // LateUpdate is called once per frame after all Update methods have been called
    //void LateUpdate()
    //{
    //    if (player != null)
    //    {
    //        // Update the camera's position to follow the player with the specified offset
    //        transform.position = player.position + offset;
    //    }
    //}


    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(player.position.x, (player.position.y) , -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}