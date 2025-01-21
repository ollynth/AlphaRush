using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            Debug.Log("Memainkan suara: " + audioSource.clip.name);
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource atau AudioClip tidak ditemukan!");
        }
    }
}
