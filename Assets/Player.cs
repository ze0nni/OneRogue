using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 centerOfMass = new Vector3(0, -2, 0);
    public float centerOfMassMovementOffset = 1;
    public float centerOfMassMovementYOffset = 1;

    public float forwardMoveForce = 10f;
    public float jumpForce = 300f;

    Rigidbody rigidbody;
    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var angle = rigidbody.transform.rotation.ToEulerAngles().y;
        var lookVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        var movDirection = new Vector2();
        if (Input.GetKey(KeyCode.D)) { movDirection.y += lookVector.x; movDirection.x += lookVector.y; }
        if (Input.GetKey(KeyCode.A)) { movDirection.y -= lookVector.x; movDirection.x -= lookVector.y; }
        if (Input.GetKey(KeyCode.W)) { movDirection.x += lookVector.x; movDirection.y += lookVector.y; }
        if (Input.GetKey(KeyCode.S)) { movDirection.x -= lookVector.x; movDirection.y -= lookVector.y; }
        movDirection.Normalize();

        rigidbody.AddForce(
            movDirection.y * forwardMoveForce,
            0,
            movDirection.x * forwardMoveForce
        );

        if (Input.GetKeyUp(KeyCode.Space) && canJump())
        {
            rigidbody.AddForce(0, jumpForce, 0);
        }

        rigidbody.centerOfMass = new Vector3(
            centerOfMass.x + movDirection.y * centerOfMassMovementOffset,
            centerOfMass.y + movDirection.magnitude * centerOfMassMovementYOffset,
            centerOfMass.z + movDirection.x * centerOfMassMovementOffset
        );
    }

    void OnDrawGizmos() {
        var rigidbody = GetComponent<Rigidbody>();

        Gizmos.color = Color.white;
        Gizmos.DrawIcon(transform.position + transform.rotation * rigidbody.centerOfMass, "food_apple.png", true);
    }

    // Jump

    private Collision currentCollision;

    internal void SetCollider(Collision collision)
    {
        this.currentCollision = collision;
    }

    private bool canJump() {
        if (null == currentCollision) {
            return false;
        }

        return true;
    }
}
