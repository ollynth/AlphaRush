using UnityEngine;
using TMPro; // Gunakan TextMeshPro

public class BookManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bookCounterText; // Referensi ke TextMeshPro
    private int bookCount; // Jumlah buku yang dikoleksi

    void Start()
    {
        // Validasi referensi
        if (bookCounterText == null)
        {
            Debug.LogError("Book Counter Text belum di-assign! Assign di inspector.", this);
            enabled = false;
            return;
        }
        
        bookCount = 0;
        UpdateBookText();
    }

    // Method untuk menambah jumlah buku
    public void AddBook()
    {
        bookCount++;
        UpdateBookText();
    }

    // Method untuk mendapatkan jumlah buku
    public int GetBookCount()
    {
        return bookCount;
    }

    // Update tampilan text
    private void UpdateBookText()
    {
        if (bookCounterText != null)
        {
            bookCounterText.text = $"Books: {bookCount}";
        }
    }
}
