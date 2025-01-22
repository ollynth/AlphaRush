using UnityEngine;

public class BookItem : CollectibleItem
{
    public AudioClip collectSound;

    protected override void Collect(PlayerController player)
    {
        if (player != null)
        {
            player.CollectBook();
            
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
        }
    }
}
//
