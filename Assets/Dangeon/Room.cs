using System;
using System.Collections;
using System.Collections.Generic;
using Dangeon;
using UnityEngine;

public class Room : MonoBehaviour
{
    public void Spawn(GeneratedRoom generatedRoom)
    {
        this.transform.position = new Vector3(
            generatedRoom.position.x * 25,
            0,
            generatedRoom.position.y * 25
        );
    }
}
