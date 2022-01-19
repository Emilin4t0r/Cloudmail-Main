using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public GameObject pauseMenu;
    public ControlsManager controlsManager;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    void TogglePause() {
        if (!pauseMenu.activeSelf) {
            pauseMenu.SetActive(true);
            controlsManager.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            controlsManager.PauseGame(true);
        } else {
            pauseMenu.SetActive(false);
            controlsManager.enabled = true;
            Cursor.lockState = controlsManager.cursorMode;
            controlsManager.PauseGame(false);
        }
    }
}
