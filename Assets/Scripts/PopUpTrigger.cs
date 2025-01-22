using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
