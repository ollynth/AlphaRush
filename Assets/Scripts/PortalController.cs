using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject WinPanel; // Referensi ke pop-up UI menang

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the portal trigger."); // Tambahkan log ini

        if (other.CompareTag("Player")) // Periksa jika objek yang masuk adalah player
        {
            Debug.Log("Player entered the portal!"); // Tambahkan log ini
            ShowWinPopup();
        }
    }

    void ShowWinPopup()
    {
        if (WinPanel != null)
        {
            Debug.Log("Activating WinPanel!"); // Tambahkan log ini
            WinPanel.SetActive(true); // Aktifkan pop-up menang
        }
        else
        {
            Debug.LogWarning("WinPanel belum disambungkan di Inspector.");
        }
    }
}
