using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;

public class FPSControl : MonoBehaviour {

    public float minX = -60f; //lower limit value for camera x-axis
    public float maxX = 60f; //upper limit value for camera x-axis
    public float sensitivity;
    public Camera cam;
    float yRot = 0f;
    float xRot = 0f;

    public float walkSpeed; //base walking speed
    public float runMultip; //multiplier for sprinting

    Rigidbody rb;
    public float jumpForce; //how fast the player jumps

    void Start() {
        rb = transform.GetComponent<Rigidbody>();
    }

    void Update() {
        LookRotations();
        Jumping();
    }
    private void FixedUpdate() {
        Movement();
    }

    void LookRotations() {
        yRot += Input.GetAxis("Mouse X") * sensitivity;
        xRot += Input.GetAxis("Mouse Y") * sensitivity;

        xRot = Mathf.Clamp(xRot, minX, maxX); //stop cam from turning over

        transform.localEulerAngles = new Vector3(0, yRot, 0); //rotate the player
        cam.transform.localEulerAngles = new Vector3(-xRot, 0, 0); //rotate the camera
    }

    void Movement() {
        float xMove;
        float zMove;       

        if (CInput.HoldKey(CInput.forward)) {
            zMove = 1 * walkSpeed / 50;
        } else if (CInput.HoldKey(CInput.backward)) {
            zMove = -1 * walkSpeed / 50;
        } else {
            zMove = 0;
        }

        if (CInput.HoldKey(CInput.right)) {
            xMove = 1 * walkSpeed / 50;
        } else if (CInput.HoldKey(CInput.left)) {
            xMove = -1 * walkSpeed / 50;
        } else {
            xMove = 0;
        }

        if (CInput.HoldKey(CInput.boost)) { //sprinting
            zMove *= runMultip;
        }

        transform.Translate(new Vector3(xMove, 0, zMove)); //move the transform
    }

    void Jumping() {
        if (CInput.KeyDown(CInput.jump)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit)) {
                if (Vector3.Distance(hit.point, transform.position) < 1.2f) {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }
        }
    }
}

