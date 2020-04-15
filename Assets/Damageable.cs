using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int MaxPoints { get; private set; }
    public int Points { get; private set; }
    public int LastFramePoints { get; private set; }

    void Update() {
        LastFramePoints = Points;
    }

    public void UpdateMaxPoints(int value, bool updatePoints) {
        this.MaxPoints = value;
        if (updatePoints) {
            Points = value;
        }
    }

    public void Hit(int Damage) {
        Points = Mathf.Clamp(Points - Damage, 0, Points);
    }
}
