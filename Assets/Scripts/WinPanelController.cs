using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanelController : MonoBehaviour
{
    public GameObject WinPanel; // Referensi ke UI panel menang

    void Start()
    {
        if (WinPanel != null)
        {
            WinPanel.SetActive(false); // Sembunyikan panel saat awal
        }
        else
        {
            Debug.LogWarning("WinPanel belum disambungkan di Inspector!");
        }
    }
}
