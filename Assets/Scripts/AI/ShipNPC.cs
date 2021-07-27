using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipNPC : MonoBehaviour {

    public GameObject shipFloor;
    public float moveSpeed = 0.05f;
    public float turnSpeed = 2f;
    public float wanderAmtX, wanderAmtZ;
    BoxCollider floorCol;
    Bounds areaBounds;
    Vector3 destination;
    bool plottingCourse;

    private void Start() {
        floorCol = shipFloor.GetComponent<BoxCollider>();
        areaBounds = floorCol.bounds;
        GetNewDestination();
    }

    private void FixedUpdate() {
        if (Vector3.Distance(transform.localPosition, destination) < 1f && !plottingCourse) {
            print("Waiting");
            StartCoroutine(WaitAndMove());
        } else {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, moveSpeed);

            Vector3 lookDir = (destination - transform.localPosition).normalized;
            Quaternion lookRot = Quaternion.LookRotation(lookDir);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, lookRot, Time.deltaTime * turnSpeed);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
    }

    IEnumerator WaitAndMove() {
        plottingCourse = true;
        float randSecs = Random.Range(1f, 3f);
        yield return new WaitForSeconds(randSecs);
        GetNewDestination();
    }

    void GetNewDestination() {
        plottingCourse = false;
        areaBounds = floorCol.bounds;
        float randomPosX = Random.Range(-areaBounds.extents.x * wanderAmtX, areaBounds.extents.x * wanderAmtX);
        float randomPosZ = Random.Range(-areaBounds.extents.z * wanderAmtZ, areaBounds.extents.z * wanderAmtZ);
        destination = new Vector3(randomPosX, 1, randomPosZ);
        print("Moving to new position");
    }
}
