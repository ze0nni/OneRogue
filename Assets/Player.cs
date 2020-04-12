﻿using System.Collections;
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

        if (Input.GetButtonDown("Jump") && CanJump()) {
            momentForce += new Vector3(0, jumpSpeed, 0);
        }

        this.controller.Move((direction + momentForce) * Time.deltaTime);

        weapon.Trigger(Input.GetButton("Fire1"));

        if (Input.GetButtonDown("Fire2")) {
            weapon.Switch();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        
    }


    bool CanJump() {
        return Physics.CheckSphere(transform.position, 1.1f, Ground, QueryTriggerInteraction.Ignore);
    }
}
