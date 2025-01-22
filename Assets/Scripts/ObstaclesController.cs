// FILE: Assets/Scripts/ObstaclesController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private float distance;
    private bool movingRight = true;
    [SerializeField] private Transform groundDetection;

    void Update()
    {
        // Move the obstacle
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Detect the edge of the platform
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            // Change direction if no ground detected
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1; // Waktu kembali normal
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}