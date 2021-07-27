using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;

public class Speeder3D : MonoBehaviour
{
	public float moveSpeed;	
	public float maxSpeedF;
	public float maxSpeedB;
	public float turboSpeed;
	public float turboCamShake;
	public float sensitivity;
	public Rigidbody rb;

	public float accelerationSpeed;
	public float turboAccSpd, whenMovingAccSpd, notMovingAccSpd;
	public float turboDuration = 5;
	float normalMaxSpeedF;

	private int[] center = new int[2];
	private float blockX;
	private float mouseX;
	private float blockY;
	private float mouseY;
	public float Xcoord;
	public float Ycoord;

	public GameObject player;
	public bool canMove;
	public bool inDockArea;
	bool hasHitWall;
	Vector3 bounceDir;
	int regionLayer = 1 << 7;
	GameObject currentRegion;

	void Start() {
		center[0] = Screen.width / 2;
		center[1] = Screen.height / 2;

		canMove = true;
		normalMaxSpeedF = maxSpeedF; // Store normal speed & acceleration values
		accelerationSpeed = 0;
	}

	void Update() {

		if (canMove) {

			blockX = Screen.width / 100f;
			mouseX = Input.mousePosition.x - center[0];
			Xcoord = mouseX / blockX;
			blockY = Screen.height / 100f;
			mouseY = Input.mousePosition.y - center[1];
			Ycoord = mouseY / blockY;

			float x = transform.eulerAngles.x;                  // \
			float y = transform.eulerAngles.y;                  // 	> Set Z rotation to 0
			transform.localEulerAngles = new Vector3(x, y, 0);  // /

			RotateShipX();
			RotateShipY();

			//Acceleration for moving forward/backward
			float moveTowards = 0;
			float changeRatePerSecond = 1 / accelerationSpeed * Time.deltaTime;

			if (CInput.HoldKey(CInput.backward)) {
				moveTowards = -maxSpeedB;
				accelerationSpeed = whenMovingAccSpd;
			} else if (CInput.HoldKey(CInput.forward)) {
				moveTowards = maxSpeedF;
				accelerationSpeed = whenMovingAccSpd;
			} else {
				accelerationSpeed = notMovingAccSpd;
			}
			
			changeRatePerSecond *= 50;
			moveSpeed = Mathf.MoveTowards(moveSpeed, moveTowards, changeRatePerSecond);

			//Boost
			if (CInput.KeyDown(CInput.boost)) {
				StartCoroutine(Boost());
            }

			transform.Translate(0, 0, moveSpeed * Time.deltaTime);

			if (rb.velocity.magnitude < .01 && !hasHitWall) {
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
			}

			//Exiting on dock
			if ((CInput.KeyDown(CInput.enter)) && inDockArea) {
				ControlsManager.instance.SwitchTo(ControlsManager.ControlEntity.Player);
				MovePlayer(ControlsManager.instance.currentDock.GetPlrSpawnPos());
				Dock dock = ControlsManager.instance.currentDock.GetComponent<Dock>();
				Dock(dock.GetDockSpot(), dock.GetDockRot(), dock.GetPlrSpawnPos());
			}
		}
	}

	IEnumerator Boost() {
		yield return new WaitForSeconds(0.35f);
		maxSpeedF = turboSpeed;
		accelerationSpeed = turboAccSpd;
		yield return new WaitForSeconds(turboDuration);
		maxSpeedF = normalMaxSpeedF;
		accelerationSpeed = whenMovingAccSpd;
	}

    private void FixedUpdate() {
		//Moving to new region
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, 1000, regionLayer) && hit.transform.CompareTag("Region")) {
			Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.black);
			GameObject region = hit.transform.gameObject;
			if (region != currentRegion) {
				RegionManager.instance.SetCurrentRegion(region);
				currentRegion = region;
			}
		}
	}

    void RotateShipX() {
		transform.Rotate(Vector3.up * (Xcoord * Time.deltaTime) * sensitivity);
	}
	void RotateShipY() {
		float angle = transform.localEulerAngles.x;
		angle = (angle > 180) ? angle - 360 : angle;
		if ((angle > -70f && Ycoord >= 0) || (angle < 70f && Ycoord <= 0))
			transform.Rotate(Vector3.left * (Ycoord * Time.deltaTime) * sensitivity);
	}

	private void OnTriggerEnter(Collider other) {
		//Docking
		if (other.transform.CompareTag("Dock")) {
			Dock dock = other.gameObject.GetComponent<Dock>();
			ControlsManager.instance.SetCurrentDock(dock.GetComponent<Dock>());
			Dock(dock.GetDockSpot(), dock.GetDockRot(), dock.GetPlrSpawnPos());
			inDockArea = true;
		}

		//Hitting Container/Gathering Resources
		if (other.transform.CompareTag("Container")) {
			ResourceManager.instance.AddResourcesRand(RegionManager.instance.GetCurrentRegionResource());
			ContainerManager.instance.RemoveFromActiveContainers(other.gameObject);
			Destroy(other.gameObject);
		}
	}
	
	private void OnTriggerExit(Collider other) {
		if (other.transform.CompareTag("Dock")) {
			inDockArea = false;
		}
	}

    private void OnCollisionEnter(Collision collision) {
		//Hitting Walls/Obstacles
		hasHitWall = true;
		bounceDir = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
		rb.AddForce(bounceDir * moveSpeed * 0.5f, ForceMode.Impulse);		
		Debug.DrawRay(transform.position, bounceDir * 10, Color.blue, 1f);
		StartCoroutine(InCollision());
	}

	IEnumerator InCollision() {
		yield return new WaitForSeconds(0.02f);
		transform.rotation = Quaternion.LookRotation(bounceDir);
		yield return new WaitForSeconds(2f);
		hasHitWall = false;
    }

    int firstDock = 0;
	void Dock(Vector3 dockPos, Quaternion dockRot, Vector3 plrSpawnPos) {
		if (firstDock > 0) {
			ControlsManager.instance.Dock(dockPos, dockRot, plrSpawnPos);
			moveSpeed = 0;
		} else {
			firstDock++;
		}
	}

	public void MovePlayer(Vector3 pos) {
		player.SetActive(true);
		player.transform.position = pos;
	}

	private void OnGUI() {
		GUI.Label(new Rect(10, 70, 100, 20), "Speed: " + moveSpeed);
		GUI.Label(new Rect(10, 90, 200, 20), "X = " + Xcoord);
		GUI.Label(new Rect(10, 110, 200, 20), "Y = " + Ycoord);
	}
}
