namespace Monsters
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Damageable))]
    public class Monster : MonoBehaviour {

        public Damageable damageable { get; private set; }

        void Start()
        {
            this.damageable = GetComponent<Damageable>();
            this.damageable.UpdateMaxPoints(10, true);
        }

        void Update() {
            if (0 == damageable.Points) {
                //TODO: Effect and exp
                DestroyObject(gameObject);
            }
        }
    }

}