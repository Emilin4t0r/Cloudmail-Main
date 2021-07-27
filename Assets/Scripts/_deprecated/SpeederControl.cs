using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeederControl : MonoBehaviour {
    public float stamina, regenSpd, turnSpd, brakingSpd, reverseSpd, maxStamina, drainAmt, acceleration;
    public AnimationCurve jumpCurve;
    bool braking, jumping;
    float currentJumpSpot = 0, jumpTimer = 0;
    public Rigidbody rb;
    float zSpeed;
    public GameObject player;
    public bool canMove;
    bool inDockArea;

    int regionLayer = 1 << 7;
    GameObject currentRegion;

    private void Start() {
        canMove = true;
    }

    void Update() {
        zSpeed = transform.InverseTransformDirection(rb.velocity).z;

        if (canMove) {

            //Acceleration
            if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > drainAmt && !jumping) {
                rb.AddForce(transform.forward * stamina, ForceMode.Impulse);
                DrainStamina();
                //Reverse
            } else if (Input.GetKey(KeyCode.S)) {
                float vThresh = 1.5f;
                float aThresh = 0.005f;
                if (zSpeed > vThresh) {
                    rb.AddForce(-rb.velocity * brakingSpd);
                    braking = true;
                } else if (zSpeed < vThresh) {
                    if (zSpeed > reverseSpd) {
                        rb.AddForce(-transform.forward * maxStamina / 10, ForceMode.Force);
                    }
                    braking = false;
                }
                if (rb.angularVelocity.magnitude > aThresh) {
                    rb.AddTorque(-rb.angularVelocity * brakingSpd);
                } else if (rb.angularVelocity.magnitude < aThresh) {
                    rb.angularVelocity = Vector3.zero;
                }
            } else if (braking) {
                braking = false;
            }

            if (Input.GetKey(KeyCode.W)) {
                if (acceleration < 3)
                    acceleration += Time.deltaTime;
                //Hold maxStamina speed
                if (rb.velocity.magnitude < maxStamina) {
                    //Straighten velocity by going towards vector that corrects forward momentum. (Think missile in space going towards target)
                    Vector3 fwd = transform.forward * maxStamina;
                    Vector3 vel = rb.velocity;
                    Vector3 corrector = fwd - vel;
                    Debug.DrawRay(transform.position, corrector * maxStamina, Color.green);
                    rb.AddForce(corrector * acceleration, ForceMode.Force);
                }
            } else {
                acceleration = 0;
            }

            //Turning
            if (Input.GetKey(KeyCode.A)) {
                rb.AddTorque(-transform.up * turnSpd, ForceMode.Force);
            } else if (Input.GetKey(KeyCode.D)) {
                rb.AddTorque(transform.up * turnSpd, ForceMode.Force);
            }

            //Jumping
            if (Input.GetKeyDown(KeyCode.Space) && !jumping) {
                jumping = true;
                jumpTimer = 0;
                currentJumpSpot = 0;
            }

            //Exiting on dock
            if (Input.GetKeyDown(KeyCode.F) && inDockArea) {
                ControlsManager.instance.SwitchTo(ControlsManager.ControlEntity.Player);
                MovePlayer(ControlsManager.instance.currentDock.GetPlrSpawnPos());
                Dock dock = ControlsManager.instance.currentDock.GetComponent<Dock>();
                Dock(dock.GetDockSpot(), dock.GetDockRot(), dock.GetPlrSpawnPos());
            }
        }
    }

    private void FixedUpdate() {
        if (stamina < maxStamina) {
            if (!braking) {
                stamina += Time.deltaTime * regenSpd;
            } else {
                stamina += Time.deltaTime * regenSpd * 2;
            }
        } else {
            stamina = maxStamina;
        }

        if (jumping) {
            float x = transform.position.x;
            float z = transform.position.z;
            transform.position = new Vector3(x, currentJumpSpot * Mathf.Abs(zSpeed), z);
            currentJumpSpot = jumpCurve.Evaluate(jumpTimer);
            jumpTimer += Time.deltaTime;
            if (jumpTimer > 1) {
                jumping = false;
                jumpTimer = 0;
                x = transform.position.x;
                z = transform.position.z;
                transform.position = new Vector3(x, 0, z);
            }
        }

        //Moving to new region
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10, regionLayer) && hit.transform.CompareTag("Region")) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.black);
            GameObject region = hit.transform.gameObject;
            if (region != currentRegion) {
                RegionManager.instance.SetCurrentRegion(region);
                currentRegion = region;
            }
        }
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
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.CompareTag("Dock")) {
            inDockArea = false;
        }
    }

    void DrainStamina() {
        stamina -= drainAmt;
    }

    int firstDock = 0;
    void Dock(Vector3 dockPos, Quaternion dockRot, Vector3 plrSpawnPos) {
        if (firstDock > 0) {                        
            ControlsManager.instance.Dock(dockPos, dockRot, plrSpawnPos);
        } else {
            firstDock++;
        }
    }
    
    public void MovePlayer(Vector3 pos) {
        player.SetActive(true);
        player.transform.position = pos;
    }
}
