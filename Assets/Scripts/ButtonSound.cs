using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip soundClip; // Audio yang akan diputar
    private AudioSource audioSource; // AudioSource yang akan digunakan untuk memutar suara

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Mengambil komponen AudioSource dari objek ini
        Button button = GetComponent<Button>(); // Mengambil komponen Button dari objek ini
        button.onClick.AddListener(PlaySound);  // Menambahkan event listener untuk tombol klik
    }

    public void PlaySound()
    {
        Debug.Log("Tombol diklik, suara akan diputar.");
        audioSource.PlayOneShot(soundClip); // Memutar suara ketika tombol ditekan
    }
    // public void PlaySound()
    // {
    //     Debug.Log("Tombol diklik, suara akan diputar.");
    //     audioSource.Play(); // Coba Play() tanpa PlayOneShot untuk uji coba
    // }
}
