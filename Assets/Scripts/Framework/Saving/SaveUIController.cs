using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveUIController : MonoBehaviour {
    public static SaveUIController instance;
    int slotNmbr = 0;

    private void Awake() {
        instance = this;
    }

    public void SelectSaveSlot(SaveSlot save) {
        slotNmbr = save.slot;
    }

    public void LoadSave() {
        if (slotNmbr != 0) {
            SaveManager.instance.LoadGame(slotNmbr);
        }
        SceneManager.LoadScene("3DFlying");
    }

    public void SaveGame() {
        if (slotNmbr != 0) {
            SaveManager.instance.SaveGame(slotNmbr);
        }
    }
}
