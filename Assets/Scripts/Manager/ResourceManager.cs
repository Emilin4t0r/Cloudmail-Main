using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages player's resources and provides reference for resource-related stuff
public class ResourceManager : MonoBehaviour {
    public static ResourceManager instance;

    public enum ResourceType { Thing, Gizmo, Gadget, Scrap }
    int r_types;
    public int[] r_amts;

    private void Start() {
        instance = this;
        r_types = Enum.GetNames(typeof(ResourceType)).Length;
        r_amts = new int[r_types];
    }

    public int[] GetAmts() {
        return r_amts;
    }

    public void SetAmts(int[] newAmts) {
        r_amts = newAmts;
    }

    public void AddResourcesRand(ResourceType mainType) {

        int[] oldAmts = new int[r_amts.Length];
        for (int i = 0; i < oldAmts.Length; ++i) {
            oldAmts[i] = r_amts[i];
        }

        int randRTypes = UnityEngine.Random.Range(0, r_types + 1);//randomize how many types of resource to give
        int[] randRTypeVals = new int[randRTypes];//make new array with that many slots
        for (int i = 0; i < randRTypes; ++i) {//randomize what the types are
            do {
                randRTypeVals[i] = UnityEngine.Random.Range(0, r_types);
            } while (randRTypeVals[i] == (int)mainType);
        }
        //randomize resource amounts to give for those types
        for (int i = 0; i < randRTypes; ++i) {
            int randRAmt = UnityEngine.Random.Range(0, 5);//randomize next resource amt to give
            r_amts[randRTypeVals[i]] += randRAmt;//add that amt to the resource-amounts array to the previously randomized resource type
        }
        int mainRAmt = UnityEngine.Random.Range(3, 10);//randomize main resource amt to give
        r_amts[(int)mainType] += mainRAmt;

        string msg = "";
        for (int i = 0; i < r_types; ++i) {
            if (oldAmts[i] < r_amts[i]) {
                msg += r_amts[i] - oldAmts[i] + " " + (ResourceType)i + ", ";
            }            
        }
        print("Added " + msg);

        ResourceList.instance.SetResourceAmounts(r_amts);
    }
}
