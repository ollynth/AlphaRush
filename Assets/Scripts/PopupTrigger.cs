using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PopupTrigger : MonoBehaviour
{
    public PopupDialog popupDialog; // Referensi ke script PopupDialog

    private bool isDialogShown = false;

    private void Start()
    {
        // Trigger dialog saat level dimulai
        if (!isDialogShown)
        {
            isDialogShown = true;
            popupDialog.ShowDialog();
        }
    }
}


