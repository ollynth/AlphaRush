using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Awake() {

        buttons[1].interactable = false;
        buttons[2].interactable = false;

        /*
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.length; i++){
            buttons[i].interactable = false;
        }

        for (int i = 0; i < unlockedLevel.length; i++){
            buttons[i].interactable = true;
        }
        */
    }


    public void OpenLevel(int levelId) {
        SceneManager.LoadSceneAsync(levelId);

    }

    
}
