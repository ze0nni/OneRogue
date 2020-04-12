using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBarInput : MonoBehaviour
{
    public Player player;
    public float mouseSens = 0.1f;

    private float xAngle;
    private float yAngle;

    void Start()
    {
        
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        xAngle = Mathf.Clamp(xAngle - mouseY * mouseSens, -90, 90);
        yAngle += mouseX * mouseSens;

        player.SetCameraAxisX(xAngle);
        player.SetCameraAxisY(yAngle);
    }
}
