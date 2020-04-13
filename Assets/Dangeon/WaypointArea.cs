namespace Dangeon
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class WaypointArea : MonoBehaviour
    {
        [System.Serializable]
        public class Waypoint
        {
            public Vector3Int position;
            public bool IsExitPoint;

            public Waypoint(int x, int y, int z) {
                this.position = new Vector3Int(x, y, z);
            }
        }

        public Waypoint[] Points;

        private void Start()
        {
        }

        void OnDrawGizmos() {
            if (null == Points) {
                return;
            }
            
            foreach (var line in
                Enumerable.Zip(
                    Points,
                    Points.Skip(1).Concat(Points.Take(2)),
                    (a, b) => (a, b)
                )
            ) {
                Gizmos.color = Color.green;
                var p1 = transform.position + transform.rotation * (line.Item1.position) + new Vector3(0.5f, 0.5f, 0.5f);
                var p2 = transform.position + transform.rotation * (line.Item2.position) + new Vector3(0.5f, 0.5f, 0.5f);
                Gizmos.DrawLine(p1,p2);
                if (line.a.IsExitPoint) {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawCube(p1, new Vector3(0.1f, 0.1f, 0.1f));
                } else {
                    Gizmos.DrawSphere(p1, 0.05f);
                }
            }
        }
    }

}