using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCamera : MonoBehaviour
{
    public Camera camera;

    private Player player;

    void Start()
    {
        this.player = GetComponent<Player>();
    }

    void Update()
    {
        var axis = player.GetMouseAxis();

        transform.rotation = Quaternion.EulerAngles(0, axis.y, 0);

        camera.transform.position = new Vector3(
            transform.position.x,
            transform.position.y + 1,
            transform.position.z
        );

        camera.transform.rotation = Quaternion.EulerAngles(axis.x, axis.y, 0);
    }
}
