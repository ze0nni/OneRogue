using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera camera;
    public float mouseSens = 0.1f;

    float xAngle = 0;
    float yAngle = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        xAngle = Mathf.Clamp(xAngle - mouseY * mouseSens, -90, 90);
        yAngle += mouseX * mouseSens;

        transform.rotation = Quaternion.EulerAngles(0, yAngle, 0);

        camera.transform.position = new Vector3(
            transform.position.x,
            transform.position.y + 1,
            transform.position.z
        );

        camera.transform.rotation = Quaternion.EulerAngles(xAngle, yAngle, 0);
    }
}
