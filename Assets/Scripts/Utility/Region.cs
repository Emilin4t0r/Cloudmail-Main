using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour {

    public ResourceManager.ResourceType resourceType;
    public GameObject questShipSpawnArea;

    public Vector3 GetShipSpawnArea() {
        return questShipSpawnArea.transform.position;        
    }
}
