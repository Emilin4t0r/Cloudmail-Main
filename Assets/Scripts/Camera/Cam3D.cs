
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam3D : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;

    void LateUpdate() {
		transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);

		// Define a target position above and behind the target transform
		Vector3 targetPosition = target.TransformPoint(new Vector3(0, 2f, -10f));

		// Smoothly move the camera towards that target position
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);		
	}
}