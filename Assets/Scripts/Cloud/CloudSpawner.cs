using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CloudSpawner : MonoBehaviour {

    public static CloudSpawner instance;

    public GameObject cloudPoof;
    public float spawnAreaSize, randHeight;
    public int maxCloudAmt;
    [SerializeField]
    int currentCloudAmt;

    private void Start() {
        instance = this;

        for (int i = 0; i < maxCloudAmt; ++i) {
            SpawnCloud();
        }
    }

    void FixedUpdate() {
        if (currentCloudAmt < maxCloudAmt) {
            SpawnCloud();
        }
    }

    void SpawnCloud() {
        GameObject cloud = Instantiate(cloudPoof, new Vector3(Random.Range(-spawnAreaSize, spawnAreaSize), transform.position.y + Random.Range(-randHeight, randHeight), Random.Range(-spawnAreaSize, spawnAreaSize)), Quaternion.identity, transform);
        float randSize = Random.Range(30, 50);
        cloud.transform.localScale = new Vector3(randSize + 0.01f, randSize, randSize);
        currentCloudAmt++;
        Destroy(cloud, cloud.GetComponent<VisualEffect>().GetFloat("lifetime") + 3);       
    }

    public void ReduceCloudAmt() {
        currentCloudAmt--;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnAreaSize);
    }
}
