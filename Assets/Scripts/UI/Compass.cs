using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform playerTrans;
    public Transform homePointer;
    public Transform homePRef;
    Vector3 cdir, pdir;

    private void FixedUpdate() {
        cdir.z = playerTrans.eulerAngles.y;
        transform.localEulerAngles = cdir;

        pdir.z = homePRef.localEulerAngles.y;
        homePointer.localEulerAngles = -pdir;
    }
}
