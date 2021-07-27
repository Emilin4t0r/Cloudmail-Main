using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public GameObject pauseMenu;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    void TogglePause() {
        if (!pauseMenu.activeSelf) {
            pauseMenu.SetActive(true);
        } else {
            pauseMenu.SetActive(false);
        }
    }
}
