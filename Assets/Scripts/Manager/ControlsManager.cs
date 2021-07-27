using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;

public class ControlsManager : MonoBehaviour {
    public enum ControlEntity { Ship, Player };
    public ControlEntity controlEntity;

    public GameObject player, speeder, plrCam, shipCam, compass;
    FPSControl plrCtrl;
    Speeder3D spdrCtrl;
    public SpeederTilt sTilt;

    Vector3 dockingPoint;
    Quaternion dockingRot;
    public bool docking, isDocked;
    Vector3 playerSpawnPosition;
    public Dock currentDock;
    float dockingTimer;

    public static ControlsManager instance;

    public float dDist, dAngle;

    private void Start() {
        instance = this;
        plrCtrl = player.GetComponent<FPSControl>();
        spdrCtrl = speeder.GetComponent<Speeder3D>();
        SwitchTo(controlEntity);
    }

    public void SwitchTo(ControlEntity entity) {
        if (entity == ControlEntity.Ship) {
            plrCtrl.enabled = false;
            spdrCtrl.enabled = true;
            plrCam.SetActive(false);
            shipCam.SetActive(true);
            compass.SetActive(true);
            isDocked = false;
            controlEntity = ControlEntity.Ship;
            InteractRay.instance.ResetLookingAt();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else if (entity == ControlEntity.Player) {
            plrCtrl.enabled = true;
            spdrCtrl.enabled = false;
            shipCam.SetActive(false);
            plrCam.SetActive(true);
            compass.SetActive(false);
            controlEntity = ControlEntity.Player;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update() {
        if (InteractRay.instance.IsLookingAt(speeder)) {
            if (CInput.KeyDown(CInput.enter)) {
                EndDocking();
                player.transform.parent = null;
                SwitchTo(ControlEntity.Ship);
                spdrCtrl.MovePlayer(speeder.transform.position);
                player.SetActive(false);
            }
        }
    }

    private void FixedUpdate() {
        if (docking) {
            spdrCtrl.canMove = false;
            dockingPoint = currentDock.GetDockSpot();
            dDist = Vector3.Distance(speeder.transform.position, dockingPoint);
            dAngle = Quaternion.Angle(speeder.transform.rotation, dockingRot);
            speeder.transform.position = Vector3.MoveTowards(speeder.transform.position, dockingPoint, dDist / 10);
            speeder.transform.rotation = Quaternion.RotateTowards(speeder.transform.rotation, dockingRot, dAngle / 10);
            if (dDist < 0.1f) {
                EndDocking();
            } else if (dDist < 1f && controlEntity != ControlEntity.Player) {
                spdrCtrl.MovePlayer(playerSpawnPosition);
                player.transform.parent = currentDock.transform.parent.transform.parent; //set player parent as ShipRotator
                SwitchTo(ControlEntity.Player);
            }
            if (Time.time > dockingTimer) {
                EndDocking();
            }
        }

        if (isDocked) {
            speeder.transform.position = currentDock.GetDockSpot();
            speeder.transform.rotation = currentDock.GetDockRot();
            /*if (sTilt.tiltAmt != 0) {
                sTilt.tiltAmt = 0;
            }*/
        }
    }

    void EndDocking() {
        docking = false;
        spdrCtrl.canMove = true;
        spdrCtrl.rb.velocity = Vector3.zero;
        spdrCtrl.rb.angularVelocity = Vector3.zero;
        speeder.transform.rotation = dockingRot;
        sTilt.tiltAmt = 0;
        isDocked = true;
    }

    public void SetCurrentDock(Dock dock) {
        currentDock = dock;
    }

    public void Dock(Vector3 dockPos, Quaternion dockRot, Vector3 plrSpawnPos) {
        docking = true;
        dockingPoint = dockPos;
        dockingRot = dockRot;
        playerSpawnPosition = plrSpawnPos;
        spdrCtrl.rb.velocity = Vector3.zero;
        spdrCtrl.rb.angularVelocity = Vector3.zero;
        dockingTimer = Time.time + 2f;
    }
}
