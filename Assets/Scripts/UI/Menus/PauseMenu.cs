using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject options, main;


    public void Return() {
        PauseManager.instance.TogglePause();
    }

    public void OpenOptionsMenu() {
        if (!options.activeSelf) {
            options.SetActive(true);
            main.SetActive(false);
        }
    }

    public void SaveGame() {
        SaveUIController.instance.SaveGame();        
    }
    
    public void MainMenu() {
        //Reset all lingering save data in case a new save is started back in main menu
        ResourceManager.instance.ResetAmounts();
        SceneManager.LoadScene("MainMenu");        
    }
}
