using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour {
    public static ContainerManager instance;

    public GameObject container;
    GameObject[] regions;
    List<GameObject> activeContainers;
    int maxContainers;
    float timeToNextRefresh;
    GameObject lastCollectedContainerSpawner;

    private void Start() {
        instance = this;
        lastCollectedContainerSpawner = null;
        regions = RegionManager.instance.regions;
        activeContainers = new List<GameObject>();
        RefreshContainers();
    }

    private void FixedUpdate() {
        //Check if time to spawn new containers or if too few left
        if (activeContainers.Count <= maxContainers / 3 || Time.time > timeToNextRefresh) {
            //Check that player isn't near any containers
            Collider[] colls = Physics.OverlapSphere(ControlsManager.instance.speeder.transform.position, 20);
            List<Collider> containersNearSpeeder = new List<Collider>();
            foreach (Collider col in colls) {
                if (col.CompareTag("Container")) {
                    containersNearSpeeder.Add(col);
                }
            }
            if (containersNearSpeeder.Count == 0) {
                RefreshContainers();
            }
        }
    }

    public void RemoveFromActiveContainers(GameObject obj) {
        activeContainers.Remove(obj);
        lastCollectedContainerSpawner = obj.transform.parent.gameObject;
    }

    void RefreshContainers() {
        foreach (GameObject container in activeContainers) {
            Destroy(container);
        }
        activeContainers.Clear();
        foreach (GameObject region in regions) {
            SpawnContainers(region);
        }
        maxContainers = activeContainers.Count;
        timeToNextRefresh = Time.time + 200;
    }

    void SpawnContainers(GameObject region) {
        List<GameObject> spawners = FindChildrenWithTag(region, "CSpawner");
        //Remove the spawner the player last picked a container from
        spawners.Remove(lastCollectedContainerSpawner);
        //Always exclude half of the spawners
        int loops = spawners.Count / 2;        
        for (int i = 0; i < loops; ++i) {
            int rand = Random.Range(0, spawners.Count);
            GameObject spawnerToBeDismissed = spawners[rand];
            spawners.Remove(spawnerToBeDismissed);
        }        
        foreach (GameObject spawner in spawners) {
            GameObject cont = Instantiate(container, spawner.transform.position, Quaternion.identity, spawner.transform);
            activeContainers.Add(cont);
        }
    }

    List<GameObject> FindChildrenWithTag(GameObject parent, string tag) {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform transform in parent.transform) {
            if (transform.CompareTag(tag)) {
                children.Add(transform.gameObject);
            }
        }
        return children;
    }
}
