using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeederTrail : MonoBehaviour {
    TrailRenderer trail;
    public Speeder3D spdr3DScript;
    public SpeederTilt spdrTiltScript;
    void Start() {
        trail = transform.GetComponent<TrailRenderer>();
        trail.emitting = false;
    }

    void FixedUpdate() {
        if (!trail.emitting) {
            if (spdr3DScript.moveSpeed >= 20 && Mathf.Abs(spdrTiltScript.tiltAmt) >= 40) {
                trail.emitting = true;
            }
        } else {
            if (spdr3DScript.moveSpeed < 18 || Mathf.Abs(spdrTiltScript.tiltAmt) < 40) {
                trail.emitting = false;
            }
        }
    }
}
