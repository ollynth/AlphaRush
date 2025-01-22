using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // atur speed player
    private float groundSpeed = 5f;
    private float jumpSpeed = 10f;

    // atur input player
    private float xInp;
    private bool spaceJump, wJump;
    public bool grounded;

    // atur jumlah jump
    private int jumpCount = 0;
    private int maxJumps = 2;

    // atur per-bukuan
    private int bookCount = 0;

    // atur HP
    public int startHP = 3; // Bisa diatur melalui Inspector
    public int currentHP;

    // Referensi HearthManager
    public HearthManager hearthManager;

    // Referensi Panel Pop-Up
    public GameObject deathPopup; // Hubungkan panel di Inspector

    // atur physics
    private Rigidbody2D body;
    private Animator anim;
    private AudioSource audioSource;
    public AudioClip jumpSound; // Assign di Inspector

    public AudioClip diedSound; // Assign di Inspector

private Vector2 lastCheckpointPosition = Vector2.zero; // Posisi default (awal level)
private bool hasCheckpoint = false; // Menentukan apakah checkpoint telah diaktifkan


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHP = startHP;
    }

    void Start()
    {
        // Pastikan hearthManager menggambar ulang nyawa saat game dimulai
        if (hearthManager != null)
        {
            hearthManager.DrawHearths(currentHP, startHP);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovements();
    }

    private void HandleMovements()
    {
        GetInput();

        if (Mathf.Abs(xInp) > 0)
        {
            body.velocity = new Vector2(xInp * groundSpeed, body.velocity.y);

            // flip the player
            if (xInp > 0.01f)
            {
                transform.localScale = Vector3.one;
            }
            else if (xInp < -0.01f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if ((spaceJump || wJump) && (grounded || jumpCount < maxJumps))
        {
            Jump();
        }

        // set animator parameters
        anim.SetBool("isWalking", xInp != 0);
    }

    private void GetInput()
    {
        // manually set the movement directions
        xInp = Input.GetAxis("Horizontal");
        spaceJump = Input.GetKeyDown(KeyCode.Space);
        wJump = Input.GetKeyDown(KeyCode.W);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        jumpCount++;
        grounded = false;
        
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Collider2D collider = contact.collider;
            Renderer renderer = collider.GetComponent<Renderer>();
            if (renderer != null && renderer.sortingLayerName == "Foreground")
            {
                grounded = true;
                jumpCount = 0; // Reset jump count when player lands on the ground
                break;
            }
        }
        if (collision.gameObject.CompareTag("WalkingObstacles"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, startHP);

        Debug.Log("Player HP: " + currentHP);

        // Update UI via HearthManager
        if (hearthManager != null)
        {
            hearthManager.DrawHearths(currentHP, startHP);
        }

        if (currentHP > 0)
        {
            RespawnAtCheckpoint(); // Jika masih ada HP, kembalikan ke checkpoint
        }
        else
        {
            EndGame(); // Jika HP habis, end game
        }
    }

    public void SetCheckpoint(Vector2 checkpointPosition)
    {
        lastCheckpointPosition = checkpointPosition;
        hasCheckpoint = true;
        Debug.Log("Checkpoint set at: " + lastCheckpointPosition);
    }

    private void RespawnAtCheckpoint()
    {
        if (hasCheckpoint)
        {
            Debug.Log("Respawning at checkpoint...");
            transform.position = lastCheckpointPosition; // Pindahkan ke checkpoint terakhir
        }
        else
        {
            Debug.Log("No checkpoint found. Respawning at start...");
            transform.position = Vector2.zero; // Jika belum ada checkpoint, mulai dari awal
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");

        if (diedSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(diedSound);
        }

        // Matikan semua suara dari AudioSource di Main Camera
        AudioSource mainCameraAudio = Camera.main?.GetComponent<AudioSource>();
        if (mainCameraAudio != null)
        {
            mainCameraAudio.Stop();
        }

        // Tampilkan pop-up Game Over
        if (deathPopup != null)
        {
            deathPopup.SetActive(true);
        }

        // Hentikan semua input pemain
        Time.timeScale = 0f; // Pause game
    }


    private void ResetToStart()
    {
        Debug.Log("Respawning at start...");
        transform.position = Vector2.zero; // Asumsikan posisi awal adalah (0, 0)
        currentHP = startHP;

        // Update UI
        if (hearthManager != null)
        {
            hearthManager.DrawHearths(currentHP, startHP);
        }

        // Pastikan pop-up mati tidak muncul
        if (deathPopup != null)
        {
            deathPopup.SetActive(false);
        }

        // Restart game
        Time.timeScale = 1f; // Resume game
    }

    public BookManager bookManager; // Hubungkan ke GameObject BookManager di Inspector

    public void CollectBook()
    {
        bookCount++;
        Debug.Log("Book Collected: " + bookCount);

        // Perbarui tampilan jumlah buku melalui BookManager
        if (bookManager != null)
        {
            bookManager.bookCount = bookCount;
        }
    }

    public int GetBookCount()
    {
        return bookCount;
    }

    public Timer timer;

<<<<<<< Updated upstream
    public void CollectClock()
    {
        Debug.Log("Clock Collected! Time reduced.");
    }
}
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
 public void CollectClock()
{
    Debug.Log("Clock Collected! Time reduced.");
}

}
=======
    public void CollectClock()
    {
        Debug.Log("Clock Collected! Time reduced.");
    }

    public void Heal(int amount)
{
    currentHP += amount;
    currentHP = Mathf.Clamp(currentHP, 0, startHP); // Jangan melebihi max HP

    Debug.Log("Player HP after healing: " + currentHP);

    // Perbarui UI nyawa melalui HearthManager
    if (hearthManager != null)
    {
        hearthManager.DrawHearths(currentHP, startHP);
    }
}
}



>>>>>>> Stashed changes
=======
    public void CollectClock()
    {
        Debug.Log("Clock Collected! Time reduced.");
    }
}
>>>>>>> Stashed changes
>>>>>>> Stashed changes
