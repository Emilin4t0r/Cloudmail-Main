using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceList : MonoBehaviour {

    public static ResourceList instance;

    Text[] resourceTexts;

    void Start() {

        instance = this;

        resourceTexts = new Text[transform.childCount];
        for (int i = 0; i < resourceTexts.Length; ++i) {
            resourceTexts[i] = transform.GetChild(i).GetComponent<Text>();
        }

        SetResourceAmounts(ResourceManager.instance.r_amts);
    }

    public void SetResourceAmounts(int[] amts) {
        for (int i = 0; i < resourceTexts.Length; ++i) {
            resourceTexts[i].text = resourceTexts[i].gameObject.name + " = " + amts[i];
        }
    }
}
