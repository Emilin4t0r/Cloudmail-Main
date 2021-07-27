using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGen : MonoBehaviour {
    public GameObject cloudBase;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GenerateCloud();
        }
    }

    void GenerateCloud() {
        GameObject cloud = Instantiate(cloudBase, Random.insideUnitSphere * 100, Random.rotation);
        float randScale = Random.Range(7.0f, 10.0f);
        cloud.transform.localScale = new Vector3(randScale, randScale, randScale);
    }
}
