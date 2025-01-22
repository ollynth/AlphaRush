using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBola : MonoBehaviour
{
    public int damage = 1; // Jumlah damage yang diberikan bola

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Periksa apakah objek yang bertabrakan adalah pemain
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ambil komponen PlayerController
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                // Berikan damage ke pemain
                player.TakeDamage(damage);
                Debug.Log("Player terkena bola! HP berkurang.");
            }
        }
    }
}


