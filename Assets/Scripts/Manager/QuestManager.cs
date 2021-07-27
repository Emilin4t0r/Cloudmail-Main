using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;

public class QuestManager : MonoBehaviour {

    public static QuestManager instance;
    public GameObject bell, package, ship;
    public Transform packageSpawn;
    public Material[] deliveryMats = new Material[3];
    public int maxQuests = 3;
    public GameObject[] pkgSlotsOnSpdr = new GameObject[3];
    int nextMat;
    public Hashtable relations;
    public int activeQuests;


    private void Start() {
        instance = this;
        relations = new Hashtable();
        nextMat = 0;
    }

    private void Update() {
        if (InteractRay.instance.IsLookingAt(bell)) {
            if (CInput.KeyDown(CInput.select) && activeQuests < maxQuests) {
                SpawnQuest(packageSpawn.transform.position, GetShipSpawnSpot());
            }
        }
    }

    void SpawnQuest(Vector3 pkgSpawn, Vector3 shipSpawn) {
        GameObject sParent = Instantiate(ship, shipSpawn, Quaternion.identity);
        GameObject p = Instantiate(package, pkgSpawn, Quaternion.identity);
        GameObject s = sParent.transform.GetChild(0).gameObject;
        s.transform.GetChild(0).GetComponent<MeshRenderer>().material = deliveryMats[nextMat];
        p.GetComponentInChildren<MeshRenderer>().material = deliveryMats[nextMat];
        //set package visually onto ship
        if (nextMat < maxQuests - 1) {
            nextMat++;
        } else {
            nextMat = 0;
        }
        relations.Add(s, p);
        activeQuests++;
    }

    public void LoadPackageOnSpdr(GameObject pkg) {
        foreach (GameObject slot in pkgSlotsOnSpdr) {
            if (slot.transform.childCount == 0) {
                pkg.transform.parent = slot.transform;
                pkg.transform.position = slot.transform.position;
                pkg.transform.rotation = Quaternion.identity;
                Destroy(pkg.transform.GetComponent<Rigidbody>());
                Destroy(pkg.transform.GetComponent<BoxCollider>());
                break;
            }
        }
    }

    Vector3 GetShipSpawnSpot() {
        //Remember to check for other ships/stuff in the way

        Vector3 final;
        GameObject[] regions = RegionManager.instance.regions;
        GameObject randRegion = regions[Random.Range(0, regions.Length)];
        Vector3 regionSpawnSpot = randRegion.GetComponent<Region>().GetShipSpawnArea();
        Vector3 rand = Random.insideUnitCircle * 100;
        final = new Vector3(regionSpawnSpot.x + rand.x, Random.Range(1f, 20f), regionSpawnSpot.z + rand.y);

        return final;
    }
}
