using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

    //public void ResetProgress()
    //{
    //    PlayerPrefs.SetInt("UnlockedLevel", 1); // Reset progress to only unlock Level 1
    //    PlayerPrefs.Save();
    //    Awake(); // Refresh the level buttons
    //}

    private void Awake() {

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // Default is 1 (first level unlocked)

        // Ensure only the correct levels are interactable
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = (i < unlockedLevel); // Unlock levels up to the stored value
        }
    }


    public void OpenLevel(int levelId) {
        SceneManager.LoadSceneAsync(levelId);

    }

    // This should be called when a level is completed
    public static void UnlockNextLevel(int currentLevel)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (currentLevel >= unlockedLevel) // Ensure we don't overwrite higher progress
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1); // Unlock next level
            PlayerPrefs.Save(); // Save changes
        }
    }

    
}
