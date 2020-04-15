using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int MaxPoints { get; private set; }
    public int Points { get; private set; }

    void Update() {
        
    }

    public void UpdateMaxPoints(int value, bool updatePoints) {
        this.MaxPoints = value;
        if (updatePoints) {
            Points = value;
        }
    }

    public void Hit(int Damage) {
        Debug.Log(Points);
        Points = Mathf.Clamp(Points - Damage, 0, Points);
        Debug.Log(Points);
    }
}
