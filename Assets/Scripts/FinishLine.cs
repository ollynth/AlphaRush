using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Untuk teks UI

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject winPopup; // Panel pop-up menang
    [SerializeField] private TextMeshProUGUI booksText; // Teks jumlah buku
    [SerializeField] private TextMeshProUGUI timeText; // Teks waktu
    [SerializeField] private Timer timer; // Referensi ke Timer
    [SerializeField] private PlayerController player; // Referensi ke PlayerController

    private void Start()
    {
        // Sembunyikan pop-up saat permainan dimulai
        if (winPopup != null)
        {
            winPopup.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Jika objek yang menyentuh adalah player
        {
            ShowWinPopup();
        }
    }

    private void ShowWinPopup()
    {
        if (winPopup != null)
        {
            // Tampilkan pop-up menang
            winPopup.SetActive(true);

            // Hentikan waktu
            Time.timeScale = 0;

            // Perbarui jumlah buku yang diambil
            if (booksText != null && player != null)
            {
                booksText.text = "Books Collected: " + player.GetBookCount();
            }

            // Perbarui waktu yang dihabiskan
            if (timeText != null && timer != null)
            {
                timeText.text = "Time Spent: " + timer.GetElapsedTimeFormatted();
            }
        }
    }

    public void ContinueToNextLevel()
    {
        Time.timeScale = 1; // Waktu kembali normal
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1; // Waktu kembali normal
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
