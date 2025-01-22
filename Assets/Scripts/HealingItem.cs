using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    [SerializeField] private int healAmount = 1; // Jumlah darah yang akan dipulihkan

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Periksa apakah objek yang menyentuh adalah player
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                // Tambahkan darah ke pemain
                player.Heal(healAmount);
                Debug.Log("Player healed by " + healAmount);

                // Hancurkan item setelah diambil
                Destroy(gameObject);
            }
        }
    }
}
