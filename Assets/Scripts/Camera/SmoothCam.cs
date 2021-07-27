using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour {
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    public Transform positionTarget, lookTarget;

    void FixedUpdate() {
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, positionTarget.position, ref velocity, smoothTime);
        transform.LookAt(lookTarget.position);
    }
}
