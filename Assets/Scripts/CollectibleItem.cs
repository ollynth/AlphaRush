using UnityEngine;

public abstract class CollectibleItem : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound;
    private bool hasTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            Collect(player);
            
            // Gunakan PlayClipAtPoint agar suara tetap terdengar meskipun object dihancurkan
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            
            Destroy(gameObject);
        }
    }

    protected abstract void Collect(PlayerController player);
}