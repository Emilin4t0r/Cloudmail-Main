using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class CloudMover : MonoBehaviour {

    public Vector3 windDirection;
    public float windSpeed;
    VisualEffect vfasset;
    float randLifetime;

    private void Awake() {
        vfasset = transform.GetComponent<VisualEffect>();
        randLifetime = Random.Range(120, 160);       
        vfasset.SetFloat("lifetime", randLifetime);
    }

    void FixedUpdate() {
        transform.Translate(windDirection * windSpeed);
    }

    private void OnDestroy() {
        CloudSpawner.instance.ReduceCloudAmt();
    }
}
