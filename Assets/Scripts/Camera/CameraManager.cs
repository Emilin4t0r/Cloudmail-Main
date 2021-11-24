using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public GameObject cam, plrCamParent, spdrCamParent, interactRay;
    public Vector3 plrCamPos, spdrCamPos;
    public Cam3D cam3Dscript;
    public static CameraManager instance;
    void Start() {
        instance = this;
    }

    public void SetCamParentToShip(bool val) {
        if (val) {
            cam.transform.parent = spdrCamParent.transform;
            cam.transform.localPosition = spdrCamPos;
            cam3Dscript.enabled = true;
            interactRay.SetActive(false);
        } else {
            cam.transform.parent = plrCamParent.transform;
            cam.transform.localPosition = plrCamPos;
            cam3Dscript.enabled = false;
            interactRay.SetActive(true);
        }
    }
}
