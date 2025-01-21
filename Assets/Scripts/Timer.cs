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
        elapsedTime = 0f; // Timer dimulai dari 0
    }

    void Update()
    {
        elapsedTime += Time.deltaTime; // Tambahkan waktu setiap frame
        UpdateTimerText(); // Perbarui teks timer
    }

    public void ReduceTime(float seconds)
    {
        elapsedTime = Mathf.Max(0, elapsedTime - seconds); // Kurangi waktu, pastikan tidak negatif
        Debug.Log("Time reduced by " + seconds + " seconds. New time: " + elapsedTime);
    }

    private void UpdateTimerText()
    {
        int minute = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minute, seconds);
    }
}
