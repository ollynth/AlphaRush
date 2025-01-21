using UnityEngine;

public abstract class CollectibleItem : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound;
    private AudioSource audioSource;
    private bool hasTriggered;

    protected virtual void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collectSound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            Collect(collision.gameObject.GetComponent<PlayerController>());
            PlayCollectSound();
            Destroy(gameObject);
        }
    }

    protected abstract void Collect(PlayerController player);

    private void PlayCollectSound()
    {
        if (audioSource != null && collectSound != null)
        {
            audioSource.enabled = true;
            audioSource.Play();
            // // Pastikan AudioSource aktif
            // if (!audioSource.isActiveAndEnabled)
            // {
            //     audioSource.enabled = true;
            // }
            
            // audioSource.PlayOneShot(collectSound);
        }
    }
}