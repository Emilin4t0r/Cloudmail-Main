using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePointerReference : MonoBehaviour
{
    public Transform home;
    private void FixedUpdate() {
        transform.LookAt(home.position);
    }
}
