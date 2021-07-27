using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject options, saveSlots, main;


    public void Return() {
        gameObject.SetActive(false);
    }

    public void OpenOptionsMenu() {
        if (!options.activeSelf) {
            options.SetActive(true);
            main.SetActive(false);
        }
    }

    public void OpenSaveSlots() {
        if (!saveSlots.activeSelf) {
            saveSlots.SetActive(true);
            main.SetActive(false);
        }
    }
    
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
