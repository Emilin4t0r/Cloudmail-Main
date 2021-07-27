using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour {
    public static RegionManager instance;
    public GameObject regionsParent;
    public GameObject boundsHztl;
    public GameObject speeder;
    public float boundsVrtl;
    bool isOutOfBoundsH;
    bool isOutOfBoundsV;
    [SerializeField]
    bool isOOB;

    public GameObject[] regions;
    GameObject currentRegion;

    Vector3 lastPos;
    float oobTimer;

    void Awake() {
        instance = this;
        currentRegion = null;

        regions = new GameObject[regionsParent.transform.childCount];
        for (int i = 0; i < regions.Length; ++i) {
            regions[i] = regionsParent.transform.GetChild(i).gameObject;
        }
    }

    private void FixedUpdate() {
        if ((speeder.transform.position.y > boundsVrtl || speeder.transform.position.y < -boundsVrtl) && !isOutOfBoundsV) {
            OutOfBoundsV(true);
        } else if ((speeder.transform.position.y < boundsVrtl && speeder.transform.position.y > -boundsVrtl) && isOutOfBoundsV) {
            OutOfBoundsV(false);
        }

        if (isOutOfBoundsH || isOutOfBoundsV) {
            if (!isOOB) {
                SaveLastPos();
                isOOB = true;
            }                    
        } else {
            if (isOOB) {
                isOOB = false;
            }
        }

        if (isOOB) {
            if (Time.time > oobTimer) {
                ReturnToMap();
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Vector3 vbUp = new Vector3(transform.position.x, transform.position.y + boundsVrtl, transform.position.z);
        Vector3 vbDown = new Vector3(transform.position.x, transform.position.y + -boundsVrtl, transform.position.z); ;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(vbUp, vbDown);
    }

    public void SetCurrentRegion(GameObject newRegion) {
        if (newRegion.transform.parent.gameObject == boundsHztl.gameObject) {
            OutOfBoundsH(true);
        } else {
            currentRegion = newRegion;
            print("Moving to new region: " + currentRegion);
            if (isOutOfBoundsH) {
                OutOfBoundsH(false);
            }
        }
    }

    public GameObject GetCurrentRegion() {
        return currentRegion;
    }

    public void OutOfBoundsH(bool isOut) {
        if (isOut) {
            isOutOfBoundsH = true;
        } else {
            isOutOfBoundsH = false;
        }
    }
    public void OutOfBoundsV(bool isOut) {
        if (isOut) {
            isOutOfBoundsV = true;
        } else {
            isOutOfBoundsV = false;
        }
    }

    void SaveLastPos() {
        Vector3 towardsRegionCenter = new Vector3(currentRegion.transform.position.x, 0, currentRegion.transform.position.z) - speeder.transform.position;
        lastPos = speeder.transform.position + towardsRegionCenter * 0.5f;
        Debug.DrawRay(speeder.transform.position, towardsRegionCenter * 0.5f, Color.green, 1.0f);
        oobTimer = Time.time + 5;
    }

    void ReturnToMap() {
        speeder.transform.position = lastPos;
    }

    public ResourceManager.ResourceType GetCurrentRegionResource() {
        return currentRegion.GetComponent<Region>().resourceType;
    }
}
