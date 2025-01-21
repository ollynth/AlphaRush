using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip buttonClip; // Tempatkan AudioClip melalui Inspector atau Resources

    void Awake()
    {
        // Tambahkan AudioSource jika belum ada
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Atur AudioSource properties
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // Tambahkan AudioClip jika tersedia
        if (buttonClip != null)
        {
            audioSource.clip = buttonClip;
        }
    }

    public void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            Debug.Log("Memainkan suara: " + audioSource.clip.name);
        }
        else
        {
            Debug.LogWarning("AudioSource atau AudioClip tidak ditemukan!");
        }
    }

    public void SetClip(AudioClip newClip)
    {
        // Mengganti AudioClip di AudioSource saat runtime
        if (audioSource != null)
        {
            audioSource.clip = newClip;
            Debug.Log("AudioClip diganti dengan: " + newClip.name);
        }
        else
        {
            Debug.LogWarning("AudioSource tidak ditemukan!");
        }
    }
}
