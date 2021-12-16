using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeederTilt : MonoBehaviour {
    public float tiltMultiplier;
    public float tiltAmt;
    Speeder3D speeder;
    private void Start() {
        speeder = transform.parent.GetComponent<Speeder3D>();
    }
    void Update() {

        if (!ControlsManager.instance.isDocked) {
            tiltAmt = speeder.Xcoord * tiltMultiplier;
            if (ControlsManager.instance.docking) {
                tiltAmt = ControlsManager.instance.dDist * 10;
            }
        }

        float x = 0;
        float y = 0;
        transform.localEulerAngles = new Vector3(x, y, -tiltAmt);
    }
}
