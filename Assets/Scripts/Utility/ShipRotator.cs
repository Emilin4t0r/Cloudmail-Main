using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotator : MonoBehaviour {
    float rotSpd;
    float shipDist;

    private void Start() {
        rotSpd = Random.Range(0.0f, 3.0f);
        float randDist = Random.Range(20.0f, 50.0f);
        shipDist = randDist;
        GameObject ship = transform.GetChild(0).gameObject;
        ship.transform.localPosition = new Vector3(shipDist, 0, 0);
    }

    private void FixedUpdate() {
        transform.Rotate(new Vector3(0, Time.deltaTime * -rotSpd, 0));
    }
}
