using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace CustomInput
{
    public class CInput : MonoBehaviour
    {

        public static KeyCode forward;
        public static KeyCode backward;
        public static KeyCode left;
        public static KeyCode right;
        public static KeyCode jump;
        public static KeyCode boost;
        public static KeyCode interact;
        public static KeyCode enter;
        public static KeyCode select;

        public static KeyCode[] codes;

        // NOTE
        // MAYBE REWORK THE WHOLE THING TO USE DICTIONARIES/HASHMAPS INSTEAD OF AN ARRAY OF VARIABLES? GETS CONFUSING.

        private void Awake()
        {
            forward = KeyCode.W;
            backward = KeyCode.S;
            left = KeyCode.A;
            right = KeyCode.D;
            jump = KeyCode.Space;
            boost = KeyCode.LeftShift;
            interact = KeyCode.E;
            enter = KeyCode.F;
            select = KeyCode.Mouse0;

            codes = new KeyCode[9];

            codes[0] = forward;
            codes[1] = backward;
            codes[2] = left;
            codes[3] = right;
            codes[4] = jump;
            codes[5] = boost;
            codes[6] = interact;
            codes[7] = enter;
            codes[8] = select;
        }

        static void RefreshKeyVars()
        {
            forward = codes[0];
            backward = codes[1];
            left = codes[2];
            right = codes[3];
            jump = codes[4];
            boost = codes[5];
            interact = codes[6];
            enter = codes[7];
            select = codes[8];
        }

        public static bool KeyDown(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
            return false;
        }
        public static bool KeyUp(KeyCode key)
        {
            if (Input.GetKeyUp(key))
            {
                return true;
            }
            return false;
        }
        public static bool HoldKey(KeyCode key)
        {
            if (Input.GetKey(key))
            {
                return true;
            }
            return false;
        }

        public static void ChangeKeyCode(string action, KeyCode newCode)
        {
            for (int i = 0; i < codes.Length; ++i) //Check if key is already in use
            {
                if (newCode == codes[i])
                {
                    //MODIFY: check if key is same as it was!
                    print("Key already in use! unbound key " + newCode);
                    codes[i] = KeyCode.None; // this is wrong.
                    return;
                }
            }
            KeyCode changee = KeyCode.None; // changing action (e.g. "forward") from string to KeyCode
            foreach (KeyCode code in codes)
            {
                if (action == code.ToString())
                {
                    changee = code;
                }
            }
            for (int i = 0; i < codes.Length; ++i)
            { //Applying new key code value to action ("forward = W")
                if (codes[i] == changee)
                {
                    codes[i] = newCode;
                    RefreshKeyVars();
                }
            }
        }
    }
}
