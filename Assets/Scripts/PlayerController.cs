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
    private float yInp;
    private bool spaceJump, wJump;
    public bool grounded;
    // atur jumlah jump
    private int jumpCount = 0;
    private int maxJumps = 2;
    // atur per-bukuan
    private int bookCount = 0;
    // atur HP
    private int startHP = 3;
    private int currentHP;
    // atur physics
    private Rigidbody2D body;
    private Animator anim;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHP = startHP;
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
        Debug.Log("Player HP: " + currentHP);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");
        
    }

    public void CollectBook()
    {
        bookCount++;
        Debug.Log("Book Collected: " + bookCount);
    }

    public void CollectClock()
    {
        Debug.Log("Clock Collected");
    }
}