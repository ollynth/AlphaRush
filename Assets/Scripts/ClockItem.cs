using UnityEngine;

public class ClockItem : CollectibleItem
{
    protected override void Collect(PlayerController player)
    {
        if (player != null)
        {
            player.CollectClock();
        }
    }
}