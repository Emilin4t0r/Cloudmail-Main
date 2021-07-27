using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {

    float turnX;
    float turnY;

    private void Update() {
        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.forward * 1);
        } else if (Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector3.back * 1);
        }
        turnX = Input.GetAxis("Mouse X");
        turnY = Input.GetAxis("Mouse Y");
        transform.eulerAngles += new Vector3(-turnY, turnX, 0);
    }
}
