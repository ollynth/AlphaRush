using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public AudioClip collectSound; // Sound yang akan diputar
    private AudioSource audioSource;

    void Start()
    {
        // Ambil AudioSource dari GameObject ini
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cek jika player menyentuh item
        if (other.CompareTag("Player"))
        {
            // Mainkan sound
            audioSource.PlayOneShot(collectSound);

            // Tambahkan logika lain seperti menambahkan skor atau menghilangkan item
            Destroy(gameObject, collectSound.length); // Hapus item setelah suara selesai
        }
    }
}
