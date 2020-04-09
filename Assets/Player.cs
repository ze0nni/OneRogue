using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 centerOfMass = new Vector3(0, -2, 0);
    public float centerOfMassMovementOffset = 1;
    public float centerOfMassMovementYOffset = 1;

    public float forwardSpeed = 10f;
    public float jumpSpeed = 30f;
    public float drag = 5f;

    public LayerMask Ground;

    Vector3 momentForce = new Vector3();

    CharacterController controller;
    
    void Start()
    {
        this.controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var mx = Input.GetAxis("Horizontal");
        var mz = Input.GetAxis("Vertical");
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
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        
    }


    bool CanJump() {
        return Physics.CheckSphere(transform.position, 1.1f, Ground, QueryTriggerInteraction.Ignore);
    }
}
