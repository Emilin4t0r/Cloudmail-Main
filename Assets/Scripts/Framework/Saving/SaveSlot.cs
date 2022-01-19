using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour {

    public int slot;
    public Text text;

    void Start() {
        CheckForSave();
    }

    public void CheckForSave() {
        string saveName = "/save" + slot.ToString() + ".cmsave";
        if (File.Exists(Application.persistentDataPath + saveName)) {
            text.text = "Save File " + slot.ToString();
        } else {
            text.text = "Empty Slot";
        }
    }

    public void SelectSave() {
        SaveUIController.instance.SelectSaveSlot(this);
    }
}
