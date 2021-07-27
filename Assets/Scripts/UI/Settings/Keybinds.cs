using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;

public class Keybinds : MonoBehaviour {
    public string[] keyCodes;
    bool settingAKey;

    private void Awake() {
        SetCodes();
    }

    public void SetCodes() {
        keyCodes = new string[9];

        keyCodes[0] = CInput.forward.ToString();
        keyCodes[1] = CInput.backward.ToString();
        keyCodes[2] = CInput.left.ToString();
        keyCodes[3] = CInput.right.ToString();
        keyCodes[4] = CInput.jump.ToString();
        keyCodes[5] = CInput.boost.ToString();
        keyCodes[6] = CInput.interact.ToString();
        keyCodes[7] = CInput.enter.ToString();
        keyCodes[8] = CInput.select.ToString();
    }
}

