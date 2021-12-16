using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour {
    private void Start() {
        Application.targetFrameRate = 144;
        Invoke("LoadNext", 0.2f);
    }

    void LoadNext() {
        SceneManager.LoadScene("MainMenu");
    }
}
