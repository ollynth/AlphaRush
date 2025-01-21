using UnityEngine;

public class BookItem : CollectibleItem
{
    public AudioClip collectSound;

    protected override void Collect(PlayerController player)
    {
        if (player != null)
        {
            Debug.Log("Player berhasil mengoleksi buku."); // Log ketika player mengoleksi buku
            
            // Mainkan suara dari prefab Book
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
                Debug.Log("Sound berhasil diputar: " + collectSound.name); // Log nama sound yang dimainkan
            }
            else
            {
                Debug.LogWarning("Sound tidak ditemukan! Pastikan collectSound sudah di-assign.");
            }

            player.CollectBook();
            Destroy(gameObject, 0.5f); // Hancurkan objek setelah dikoleksi
        }
        else
        {
            Debug.LogError("Player tidak ditemukan saat mencoba mengoleksi buku!");
        }
    }
}
//
