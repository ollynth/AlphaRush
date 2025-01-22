using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private bool isUnlocked;
    private enum CPState { Locked, Unlocking, Unlocked }
    private CPState stateCP = CPState.Locked;
    private Animator anim;
    private AudioSource audioSource; // Tambahkan variabel AudioSource

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Ambil komponen AudioSource
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isUnlocked)
        {
            UnlockCheckpoint();
        }
    }

    private void UnlockCheckpoint()
    {
        isUnlocked = true;
        Debug.Log("Checkpoint unlocked: " + gameObject.name);
        PlaySound(); // Mainkan sound saat checkpoint di-unlock
        StartCoroutine(AnimateCheckpoint());

        // Kirim posisi checkpoint ke PlayerController
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetCheckpoint(transform.position);
            }
        }
    }

    private IEnumerator AnimateCheckpoint()
    {
        stateCP = CPState.Unlocking;
        anim.SetInteger("CPState", (int)CPState.Unlocking);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + .5f);
        stateCP = CPState.Unlocked;
        anim.SetInteger("CPState", (int)CPState.Unlocked);
    }

    private void CheckpointStates()
    {
        switch (stateCP)
        {
            case CPState.Locked:
                // Implement your logic for locked checkpoint
                break;
            case CPState.Unlocking:
                // Implement your logic for unlocking checkpoint
                break;
            case CPState.Unlocked:
                // Implement your logic for unlocked checkpoint
                break;
            default:
                break;
        }

        anim.SetInteger("CPState", (int)stateCP);
    }

    // Method untuk memainkan sound
    private void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play(); // Mainkan audio clip
        }
        else
        {
            Debug.LogWarning("AudioSource atau AudioClip tidak tersedia pada " + gameObject.name);
        }
    }
}
