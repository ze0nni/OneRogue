using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Weapon))]
public class Player : MonoBehaviour
{
    public Vector3 centerOfMass = new Vector3(0, -2, 0);
    public float centerOfMassMovementOffset = 1;
    public float centerOfMassMovementYOffset = 1;

    public float forwardSpeed = 10f;
    public float jumpSpeed = 30f;
    public float drag = 5f;

    private float horizontalAxis = 0;
    private float verticalAxis = 0;
    private float mouseAxisX = 0;
    private float mouseAxisY = 0;

    private bool buttonA;
    private bool buttonB;

    public LayerMask Ground;

    Vector3 momentForce = new Vector3();

    CharacterController controller;
    Weapon weapon;
    
    void Start()
    {
        this.controller = GetComponent<CharacterController>();
        this.weapon = GetComponent<Weapon>();
    }

    public void SetHorisontalAxis(float value) {
        this.horizontalAxis = value;
    }

    public void SetVerticalAxis(float value)
    {
        this.verticalAxis = value;
    }

    public void SetCameraAxisX(float value) {
        this.mouseAxisX += value;
    }

    public void SetCameraAxisY(float value)
    {
        this.mouseAxisY += value;
    }

    public Vector2 GetMouseAxis() {
        return new Vector2(mouseAxisX, mouseAxisY);
    }

    public void UpdateButtonA(bool value) {
        buttonA = value;
    }

    public void UpdateButtonB(bool value)
    {
        buttonB = value;
        Time.timeScale = value ? 0.1f : 1;
    }

    void Update()
    {
        var mx = this.horizontalAxis;
        var mz = this.verticalAxis;
        
        //TODO: Normalize
        var direction = (transform.right * mx + transform.forward * mz) * forwardSpeed + Physics.gravity;

        //
        var spend = Time.deltaTime * drag;
        var forceLength = momentForce.magnitude;
        if (forceLength <= spend) {
            momentForce = Vector3.zero;
        } else {
            momentForce = momentForce.normalized * (forceLength - spend);
        }

        this.controller.Move((direction + momentForce) * Time.deltaTime);

        weapon.Trigger(buttonA);
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        
    }


    bool CanJump() {
        return Physics.CheckSphere(transform.position, 1.1f, Ground, QueryTriggerInteraction.Ignore);
    }
}
