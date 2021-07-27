using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour {
    public GameObject dockSpot, plrSpawnPos;

    public Vector3 GetDockSpot() {
        return dockSpot.transform.position;
    }

    public Vector3 GetPlrSpawnPos() {
        return plrSpawnPos.transform.position;
    }

    public Quaternion GetDockRot() {
        return dockSpot.transform.rotation;
    }
}
