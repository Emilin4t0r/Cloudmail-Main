using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;

public class ControlsManager : MonoBehaviour {
    public enum ControlEntity { Ship, Player };
    public ControlEntity controlEntity;

    public GameObject player, speeder, compass;
    FPSControl plrCtrl;
    Speeder3D spdrCtrl;
    public SpeederTilt sTilt;
    public CursorLockMode cursorMode;

    Vector3 dockingPoint;
    Quaternion dockingRot;
    public bool docking, isDocked;
    Vector3 playerSpawnPosition;
    public Dock currentDock;
    float dockingTimer;

    public static ControlsManager instance;

    public float dDist, dAngle;

    private void Awake() {
        instance = this;
    }

    private void Start() {        
        plrCtrl = player.GetComponent<FPSControl>();
        spdrCtrl = speeder.GetComponent<Speeder3D>();
        SwitchTo(controlEntity);        
    }

    public void PauseGame(bool state) {
        if (controlEntity == ControlEntity.Player) {
            plrCtrl.enabled = !state;
        } else if (controlEntity == ControlEntity.Ship) {
            spdrCtrl.enabled = !state;
        }        
    }

    public void SwitchTo(ControlEntity entity) {
        if (entity == ControlEntity.Ship) {
            plrCtrl.enabled = false;
            StartCoroutine(SpdrAnimWaiter());
            CameraManager.instance.SetCamParentToShip(true);
            compass.SetActive(true);            
            controlEntity = ControlEntity.Ship;
            InteractRay.instance.ResetLookingAt();
            ChangeCursorLockState(CursorLockMode.Confined);
            isDocked = false;
        } else if (entity == ControlEntity.Player) {            
            spdrCtrl.enabled = false;
            StartCoroutine(PlrAnimWaiter());
            CameraManager.instance.SetCamParentToShip(false);
            compass.SetActive(false);
            controlEntity = ControlEntity.Player;
            ChangeCursorLockState(CursorLockMode.Locked);
        }
    }

    IEnumerator SpdrAnimWaiter() {
        yield return new WaitForSeconds(0.5f);
        spdrCtrl.enabled = true;
        spdrCtrl.canMove = true;
    }

    IEnumerator PlrAnimWaiter() {
        yield return new WaitForSeconds(0.0f);
        plrCtrl.enabled = true;
    }

    void ChangeCursorLockState(CursorLockMode mode) {
        Cursor.lockState = mode;
        cursorMode = mode;
        if (mode == CursorLockMode.Locked) {
            Cursor.visible = false;
        } else {
            Cursor.visible = true;
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
            speeder.transform.rotation = Quaternion.RotateTowards(speeder.transform.rotation, dockingRot, dAngle / 20);
            if (dAngle < 0.1f) {
                EndDocking();
            } else if (dAngle < 5f && controlEntity != ControlEntity.Player) {
                spdrCtrl.MovePlayer(playerSpawnPosition);
                player.transform.parent = currentDock.transform.parent.transform.parent; //set player parent as ShipRotator
                SwitchTo(ControlEntity.Player);
            }
            if (Time.time > dockingTimer) {
                EndDocking();
                if (controlEntity != ControlEntity.Player) {
                    spdrCtrl.MovePlayer(playerSpawnPosition);
                    player.transform.parent = currentDock.transform.parent.transform.parent; //set player parent as ShipRotator
                    SwitchTo(ControlEntity.Player);
                }
            }
        }

        if (isDocked) {
            speeder.transform.position = currentDock.GetDockSpot();
            speeder.transform.rotation = currentDock.GetDockRot();            
        }
    }

    void EndDocking() {
        docking = false;
        spdrCtrl.canMove = false;
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
        dockingTimer = Time.time + 1.5f;
    }
}
