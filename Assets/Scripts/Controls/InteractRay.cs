using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRay : MonoBehaviour {
    public static InteractRay instance;
    public float range;

    GameObject lookingAt;

    int layerMask = 1 << 6;

    private void Start() {
        instance = this;
    }

    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, layerMask) && hit.transform.CompareTag("Interactable")) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            lookingAt = hit.transform.gameObject;
        } else if (lookingAt != null) {
            lookingAt = null;
        }
    }

    public bool IsLookingAt(GameObject obj) {
        if (lookingAt == obj) {
            return true;
        }
        return false;
    }

    public void ResetLookingAt() {
        lookingAt = null;
    }
}
