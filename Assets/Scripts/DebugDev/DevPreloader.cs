using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevPreloader : MonoBehaviour {
    void Awake() {
        GameObject check = GameObject.Find("DDOL");
        if (check == null) { UnityEngine.SceneManagement.SceneManager.LoadScene("PreloadScene"); }
    }
}
