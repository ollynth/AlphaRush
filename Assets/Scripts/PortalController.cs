using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the portal!");
            ShowWinPopup();
        }
    }

    private void ShowWinPopup()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f; // Pause game ketika menang
        }
        else
        {
            Debug.LogError("WinPanel reference is missing!");
        }
    }
}
