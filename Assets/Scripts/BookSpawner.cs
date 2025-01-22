using UnityEngine;

public class BookSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bookPrefab; // Prefab buku yang akan di-spawn
    [SerializeField] private int maxBooks = 7; // Jumlah maksimal buku
    
    // Area spawn (batas area dimana buku bisa muncul)
    [SerializeField] private float minX = -10f; // Batas kiri
    [SerializeField] private float maxX = 10f;  // Batas kanan
    [SerializeField] private float minY = -3f;  // Batas bawah
    [SerializeField] private float maxY = 3f;   // Batas atas

    private void Start()
    {
        SpawnBooks();
    }

    private void SpawnBooks()
    {
        for (int i = 0; i < maxBooks; i++)
        {
            SpawnBook();
        }
    }

    private void SpawnBook()
    {
        int maxAttempts = 50; // Batasi jumlah percobaan untuk mencegah infinite loop
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            // Generate posisi random dalam batas area yang ditentukan
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

            // Cek apakah posisi valid
            if (IsValidPosition(randomPosition))
            {
                Instantiate(bookPrefab, randomPosition, Quaternion.identity);
                return;
            }

            attempts++;
        }

        Debug.LogWarning("Tidak bisa menemukan posisi valid untuk spawn buku setelah " + maxAttempts + " percobaan");
    }

    private bool IsValidPosition(Vector3 position)
    {
        // Cek apakah ada collider lain di posisi tersebut
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f);
        
        // Cek apakah posisi berada di atas platform/tanah
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 2f);
        
        // Posisi valid jika:
        // 1. Tidak ada collider non-trigger di posisi tersebut
        // 2. Ada platform/tanah di bawahnya (dalam jarak 2 unit)
        foreach (Collider2D collider in colliders)
        {
            if (!collider.isTrigger)
            {
                return false;
            }
        }

        // Pastikan ada platform di bawah dan tidak terlalu jauh
        if (hit.collider != null && hit.distance < 2f)
        {
            // Sesuaikan posisi agar sedikit di atas platform
            position.y = hit.point.y + 1f;
            return true;
        }

        return false;
    }

    // Optional: Method untuk visualisasi area spawn di editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = new Vector3((maxX + minX) / 2, (maxY + minY) / 2, 0);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, 0);
        Gizmos.DrawWireCube(center, size);
    }
} 