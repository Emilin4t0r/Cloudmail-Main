using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    public GameObject[] clusters = new GameObject[2];

    void Start() {
        //Pick 2-5 slots, inst. random cluster objects as their children, rotate those clusters randomly.
        int newClouds = Random.Range(2, 6);
        for (int i = 0; i < newClouds; ++i) {
            int randAxis = Random.Range(0, 3);
            Vector3 randPos = Vector3.zero;
            /*if (randAxis == 0) {
                randPos = new Vector3(Random.Range(1.0f, 5f), 0, 0);
            } else if (randAxis == 1) {
                randPos = new Vector3(0, Random.Range(1.0f, 5f), 0);
            } else if (randAxis == 2) {
                randPos = new Vector3(0, 0, Random.Range(1.0f, 5f));
            }*/
            randPos = new Vector3(Random.Range(1.0f, 5f), 0, 0);

            Quaternion randRot = Random.rotation;
            Instantiate(clusters[Random.Range(0, clusters.Length)], gameObject.transform.position + randPos, randRot, gameObject.transform);
        }
    }
}
