using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWheel : MonoBehaviour
{
    public Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void OnCollisionEnter(Collision collision)
    {
        player.SetCollider(collision);
    }

    void OnCollisionExit(Collision collision)
    {
        player.SetCollider(null);
    }
}
