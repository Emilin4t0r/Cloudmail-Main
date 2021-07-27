using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour {

    public int slot;
    public Text text;
    bool hasSaveFile;

    void Start() {
        string saveName = "/save" + slot.ToString() + ".cmsave";
        if (File.Exists(Application.persistentDataPath + saveName)) {
            text.text = "Load Save " + slot.ToString();
            hasSaveFile = true;
        } else {
            text.text = "Start New \nSave";
            hasSaveFile = false;
        }
    }

    public void StartSave() {
        if (hasSaveFile) {
            SaveManager.instance.LoadGame(slot);
        }
        SceneManager.LoadScene("3DFlying");
    }

    public void SaveGame() {
        SaveManager.instance.SaveGame(slot);
    }
}
