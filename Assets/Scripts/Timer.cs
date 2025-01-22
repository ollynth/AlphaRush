using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText; // Teks untuk menampilkan waktu
    private float elapsedTime; // Total waktu yang berlalu (berjalan seperti biasa)

    void Start()
    {
        if (timerText == null)
        {
            timerText = FindObjectOfType<TextMeshProUGUI>();
            if (timerText == null)
            {
                Debug.LogError("No TextMeshProUGUI component found in the scene!");
            }
        }

        elapsedTime = 0f;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime; // Tambahkan waktu setiap frame
        UpdateTimerText(); // Perbarui teks timer
    }

    // Fungsi untuk mengurangi waktu
    public void ReduceTime(float seconds)
    {
        elapsedTime = Mathf.Max(0, elapsedTime - seconds); // Kurangi waktu, pastikan tidak negatif
        Debug.Log("Time reduced by " + seconds + " seconds. New time: " + elapsedTime);
    }

    // Fungsi untuk mendapatkan waktu yang telah berlalu dalam format menit:detik
    public string GetElapsedTimeFormatted()
    {
        int minute = Mathf.FloorToInt(elapsedTime / 60); // Hitung menit
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // Hitung detik
        return string.Format("{0:00}:{1:00}", minute, seconds); // Format waktu
    }

    // Fungsi untuk memperbarui teks timer
    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = GetElapsedTimeFormatted();
        }
        else
        {
            Debug.LogError("timerText is not assigned in the Inspector!", this);
        }
    }
}
