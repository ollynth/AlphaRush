using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDialog : MonoBehaviour
{
    public GameObject dialogAwal; // Referensi ke panel DialogAwal

    public void ShowDialog()
    {
        // Tampilkan dialog dan hentikan waktu
        if (dialogAwal != null)
        {
            dialogAwal.SetActive(true);
            Time.timeScale = 0f; // Pause game
        }
    }

    public void CloseDialog()
    {
        // Sembunyikan dialog dan lanjutkan waktu
        if (dialogAwal != null)
        {
            dialogAwal.SetActive(false);
            Time.timeScale = 1f; // Resume game
        }
    }
}


