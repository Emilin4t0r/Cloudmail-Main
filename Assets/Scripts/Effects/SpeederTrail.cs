using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeederTrail : MonoBehaviour {
    TrailRenderer trail;
    public SpeederTilt spdrTiltScript;
    void Start() {
        trail = transform.GetComponent<TrailRenderer>();
        trail.emitting = false;
    }

    void FixedUpdate() {
        if (!trail.emitting) {
            if (Mathf.Abs(spdrTiltScript.tiltAmt) >= 40) {
                trail.emitting = true;
            }
        } else {
            if (Mathf.Abs(spdrTiltScript.tiltAmt) < 40) {
                trail.emitting = false;
            }
        }
    }
}
