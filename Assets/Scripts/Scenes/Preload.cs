using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour {
    private void Start() {
        Invoke("LoadNext", 0.2f);
    }

    void LoadNext() {
        SceneManager.LoadScene("MainMenu");
    }
}
