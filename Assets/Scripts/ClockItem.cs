using UnityEngine;

public class ClockItem : CollectibleItem
{
    [SerializeField] private float timeReduction = 5f; // Waktu yang dikurangi (default 5 detik)

    protected override void Collect(PlayerController player)
    {
        if (player != null && player.timer != null)
        {
            player.timer.ReduceTime(timeReduction); // Kurangi waktu pada Timer
            player.CollectClock(); // Tambahkan logika lain seperti log atau animasi
        }
        Destroy(gameObject); // Hancurkan clock setelah dikoleksi
    }
}
