using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // Teks untuk menampilkan waktu
    private float elapsedTime; // Total waktu yang berlalu (berjalan seperti biasa)

    void Start()
    {
        // Validasi timerText pada Start
        if (timerText == null)
        {
            Debug.LogError("Timer Text reference is missing! Please assign it in the Inspector.", this);
            enabled = false; // Nonaktifkan komponen jika timerText tidak ada
            return;
        }
        
        elapsedTime = 0f; // Timer dimulai dari 0
    }

    void Update()
    {
        if (timerText == null) return; // Extra safety check
        
        elapsedTime += Time.deltaTime; // Tambahkan waktu setiap frame
        UpdateTimerText(); // Perbarui teks timer
    }

    // Fungsi untuk mengurangi waktu
    public void ReduceTime(float seconds)
    {
        elapsedTime = Mathf.Max(0, elapsedTime - seconds); // Kurangi waktu, pastikan tidak negatif
        Debug.Log("Time reduced by " + seconds + " seconds. New time: " + elapsedTime);
        UpdateTimerText(); // Update display setelah pengurangan waktu
    }

    // Fungsi untuk mendapatkan waktu yang telah berlalu dalam format menit:detik
    public string GetElapsedTimeFormatted()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // Hitung menit
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // Hitung detik
        return string.Format("{0:00}:{1:00}", minutes, seconds); // Format waktu
    }

    // Fungsi untuk memperbarui teks timer
    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = GetElapsedTimeFormatted(); // Gunakan fungsi GetElapsedTimeFormatted untuk memperbarui teks
        }
    }
}
