using UnityEngine;

public class BookItem : CollectibleItem
{
    protected override void Collect(PlayerController player)
    {
        if (player != null)
        {
            player.CollectBook();
        }
    }
}