using UnityEngine;
using UnityEngine.SceneManagement;

public class BacksoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // Jika ada objek dengan tag "Backsound" yang sudah ada, hancurkan objek ini
        if (GameObject.FindGameObjectsWithTag("Backsound").Length > 1)
        {
            Destroy(gameObject); // Hancurkan objek baru yang dibuat
        }
        else
        {
            // Menandai objek agar tidak dihancurkan saat scene berpindah
            DontDestroyOnLoad(gameObject);
            gameObject.tag = "Backsound";  // Menetapkan tag untuk mencari objek ini nantinya
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Menghentikan backsound saat scene baru dimuat
        audioSource.Stop();
    }

    void OnDestroy()
    {
        // Pastikan listener dihapus ketika objek dihancurkann
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
