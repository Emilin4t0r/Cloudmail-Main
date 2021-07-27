using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInput;

public class KeybindButton : MonoBehaviour {
    public Text btnText;
    Keybinds keybinds;

    bool settingKey;
    int myIndex = 0;

    //Initialization of correct input key value
    private void Start() {
        keybinds = transform.parent.GetComponent<Keybinds>();

        for (int i = 0; i < keybinds.transform.childCount; ++i) {
            if (keybinds.transform.GetChild(i).gameObject == gameObject) {
                myIndex = i;
                continue;
            }
        }

        btnText.text = keybinds.keyCodes[myIndex];
    }

    private void Update() {
        if (settingKey) {
            if (Input.anyKeyDown) {
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode))) {
                    if (Input.GetKey(kcode)) {
                        //Get which key was pressed (kcode)
                        CInput.ChangeKeyCode(keybinds.keyCodes[myIndex], kcode);
                        keybinds.SetCodes();
                        btnText.text = keybinds.keyCodes[myIndex];
                        settingKey = false;
                    }
                }
            }
        }
    }

    public void ChangeKey() {
        btnText.text = "Press a key";
        settingKey = true;
    }
}
