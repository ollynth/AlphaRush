using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    public int bookCount; // Jumlah buku yang dikoleksi
    public Text bookText; // UI Text untuk menampilkan jumlah buku

    void Start()
    {
        bookCount = 0; // Awal jumlah buku adalah 0
        UpdateBookText();
    }

    void Update()
    {
        // Jika jumlah buku diperbarui, perbarui teks
        UpdateBookText();
    }

    private void UpdateBookText()
    {
        bookText.text = "Books Collected: " + bookCount.ToString();
    }
}
