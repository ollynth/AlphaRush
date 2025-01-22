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

    [SerializeField] private BookManager bookManager; // Ubah menjadi SerializeField dan private

    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.4f, 0.1f);
    [SerializeField] private Vector2 groundCheckOffset = new Vector2(0, -0.5f);

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
        CheckGrounded(); // Panggil di setiap frame
        HandleMovements();
    }

    private void HandleMovements()
    {
        GetInput();

        float moveSpeed = groundSpeed;
        float targetVelocityX = xInp * moveSpeed;

        // Smooth out the movement
        float smoothing = grounded ? 0.1f : 0.2f;
        body.velocity = new Vector2(
            Mathf.Lerp(body.velocity.x, targetVelocityX, 1 - smoothing),
            body.velocity.y
        );

        // Flip player
        if (xInp > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (xInp < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Jump handling
        if ((spaceJump || wJump) && (grounded || jumpCount < maxJumps))
        {
            Jump();
        }

        anim.SetBool("isWalking", Mathf.Abs(xInp) > 0.01f);
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
        float jumpVelocity = jumpSpeed;
        
        // Jika melompat saat bergerak, tambahkan sedikit momentum horizontal
        if (Mathf.Abs(body.velocity.x) > 0.1f)
        {
            float horizontalBoost = Mathf.Sign(body.velocity.x) * 2f;
            body.velocity = new Vector2(body.velocity.x + horizontalBoost, jumpVelocity);
        }
        else
        {
            body.velocity = new Vector2(body.velocity.x, jumpVelocity);
        }

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

    private void TakeDamage(int damage)
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

    public void CollectBook()
    {
        bookCount++;

        if (bookManager != null)
        {
            bookManager.AddBook();
        }
    }

    public int GetBookCount()
    {
        return bookCount;
    }

    public Timer timer;

    public void CollectClock()
    {
        Debug.Log("Clock Collected! Time reduced.");
    }

    private void CheckGrounded()
    {
        // Posisi untuk ground check
        Vector2 position = (Vector2)transform.position + groundCheckOffset;
        
        // Gunakan BoxCast untuk ground check yang lebih akurat
        RaycastHit2D hit = Physics2D.BoxCast(
            position,                  // origin
            groundCheckSize,           // size
            0f,                       // angle
            Vector2.down,             // direction
            groundCheckDistance,       // distance
            groundLayer               // layer mask
        );

        grounded = hit.collider != null;
        
        if (grounded)
        {
            jumpCount = 0;
            
            // Cek apakah player di pinggiran
            float platformWidth = 0.1f;
            bool leftEdge = !Physics2D.Raycast(position + Vector2.left * platformWidth, Vector2.down, groundCheckDistance, groundLayer);
            bool rightEdge = !Physics2D.Raycast(position + Vector2.right * platformWidth, Vector2.down, groundCheckDistance, groundLayer);
            
            // Jika di pinggiran, berikan sedikit dorongan ke tengah platform
            if (leftEdge && body.velocity.x < 0)
            {
                body.velocity = new Vector2(body.velocity.x * 0.8f, body.velocity.y);
            }
            else if (rightEdge && body.velocity.x > 0)
            {
                body.velocity = new Vector2(body.velocity.x * 0.8f, body.velocity.y);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Visualisasi ground check
        Gizmos.color = Color.green;
        Vector2 position = (Vector2)transform.position + groundCheckOffset;
        Gizmos.DrawWireCube(position, groundCheckSize);
        Gizmos.DrawLine(position, position + Vector2.down * groundCheckDistance);
    }
}
