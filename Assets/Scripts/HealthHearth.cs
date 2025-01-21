using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHearth : MonoBehaviour
{
    public Sprite fullHearth, emptyHearth;
    Image hearthImage;

    private void Awake()
    {
        hearthImage = GetComponent<Image>();
    }

    public void SetHearthImage(HearthStatus status)
    {
        switch (status)
        {
            case HearthStatus.Empty:
                hearthImage.sprite = emptyHearth; // Ubah Sprite menjadi sprite (huruf kecil)
                break;
            case HearthStatus.Full:
                hearthImage.sprite = fullHearth; // Ubah Sprite menjadi sprite (huruf kecil)
                break;
        }
    }
}

public enum HearthStatus
{
    Empty = 0,
    Full = 1
}
