using UnityEngine;

public class ClockItem : CollectibleItem
{
    public AudioClip collectSound;
    [SerializeField] private float timeReduction = 5f; // Waktu yang dikurangi (default 5 detik)

    protected override void Collect(PlayerController player)
    {
        if (player != null && player.timer != null)
        {
            player.timer.ReduceTime(timeReduction); // Kurangi waktu pada Timer
            player.CollectClock(); // Tambahkan logika lain seperti log atau animasi

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
        }
        Destroy(gameObject); // Hancurkan clock setelah dikoleksi
    }
}
