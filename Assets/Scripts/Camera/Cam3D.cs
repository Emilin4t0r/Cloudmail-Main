
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam3D : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.3f;
	public Speeder3D spdr;

    void LateUpdate() {
		transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);

		// Define a target position above and behind the target transform
		Vector3 targetPosition = target.TransformPoint(new Vector3(0, 2f, -10f));

		// Smoothing time relative to speeder speed (needs to be 0.08 when speed is 20)
		smoothTime = spdr.moveSpeed / 250f;

		// Smoothly move the camera towards that target position
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, smoothTime);
	}
}