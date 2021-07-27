using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour {

    public static SaveManager instance;

    public ResourceManager rm;

    private void Start() {
        instance = this;
    }

    private Save CreateSaveGameObject() {
        Save save = new Save();
        save._r_amts = rm.GetAmts();
        /* THINGS TO SAVE:
         * days spent
         * ship upgrades
         * cosmetics
         * current world
         */

        return save;
    }

    public void SaveGame(int slot) {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        string saveName = "/save" + slot.ToString() + ".cmsave";
        FileStream file = File.Create(Application.persistentDataPath + saveName);
        bf.Serialize(file, save);
        file.Close();
    }

    public void LoadGame(int slot) {
        string saveName = "/save" + slot.ToString() + ".cmsave";

        if (File.Exists(Application.persistentDataPath + saveName)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + saveName, FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            rm.SetAmts(save._r_amts);
        }
    }
}
