using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour {

    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
    public GameObject ship;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    Quaternion localRot;

    Vector3 lastMouseCoordinate = Vector3.zero;
    Quaternion lastRot;
    float timeToReCenter;
    public bool mouseMoved;
    void Start() {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        timeToReCenter = Time.time + 2;
    }

    void Update() {
        transform.position = ship.transform.TransformPoint(new Vector3(0, 3, 0));

        /*
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Vector3 mDelta = Input.mousePosition - lastMouseCoordinate;
        float mInput = mDelta.magnitude; //How much the mouse is moving
        float angle = Quaternion.Angle(transform.rotation, ship.transform.rotation);

        if (mouseMoved && angle < 0.3f && Time.time > timeToReCenter) {
            mouseMoved = false;            
        }
        if (mInput > 0.05f) {
            localRot = Quaternion.Euler(rotX, rotY, 0.0f);
            timeToReCenter = Time.time + 1.5f;
            mouseMoved = true;
        } else if (mouseMoved && Time.time > timeToReCenter && angle > 0.3f) {
            float speed = Vector3.Distance(transform.rotation.eulerAngles, ship.transform.rotation.eulerAngles) / 50 + 0.5f;
            localRot = Quaternion.RotateTowards(transform.rotation, ship.transform.rotation, speed);
        }

        if (localRot != lastRot) { // When mouse moving
            transform.rotation = localRot; // Camera turns relative to speeder
        }

        if (!mouseMoved) {
            transform.rotation = ship.transform.rotation;
            rotY = transform.eulerAngles.y;
            rotX = transform.eulerAngles.x;
        }

        lastMouseCoordinate = Input.mousePosition;
        lastRot = localRot;
        */
    }
}
