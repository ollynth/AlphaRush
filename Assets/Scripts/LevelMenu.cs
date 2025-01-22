using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Awake() 
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        
        // Nonaktifkan semua button terlebih dahulu
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        // Aktifkan button sesuai level yang sudah terbuka
        for (int i = 0; i < unlockedLevel; i++)
        {
            if (i < buttons.Length)
            {
                buttons[i].interactable = true;
            }
        }
    }

    public void OpenLevel(int levelId) 
    {
        if (levelId >= 0 && levelId < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(levelId);
        }
        else
        {
            Debug.LogError($"Invalid level ID: {levelId}");
        }
    }
}
