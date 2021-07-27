using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeederTilt : MonoBehaviour {
    public float tiltMultiplier;
    public float tiltAmt;
    public GameObject parent;
    Speeder3D speeder;
    private void Start() {
        speeder = parent.GetComponent<Speeder3D>();
    }
    void FixedUpdate() {
        transform.position = parent.transform.position;

        if (!ControlsManager.instance.isDocked) {
            tiltAmt = speeder.Xcoord * tiltMultiplier;
            if (ControlsManager.instance.docking) {
                tiltAmt = ControlsManager.instance.dDist * 10;
            }
        }
        float x = parent.transform.localEulerAngles.x;
        float y = parent.transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(x, y, -tiltAmt);
    }
}
