using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSaveGame : MonoBehaviour {

    public void StartGame() {
        SaveUIController.instance.LoadSave();
    }

    public void SaveGame() {
        SaveUIController.instance.SaveGame();
    }
}
