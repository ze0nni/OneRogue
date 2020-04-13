#if UNITY_EDITOR

namespace Dangeon
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(WaypointArea))]
    public class WaypointEditor : Editor
    {

        Vector3 pos;

        private void OnSceneGUI()
        {
            var area = (WaypointArea)target;

            if (null == area.Points) {
                return;
            }

            for (var i = 0; i < area.Points.Length; i++)
            {
                var point = area.Points[i];

                Handles.SphereHandleCap(1, point.position, Quaternion.identity, 1, EventType.Ignore);

                var nextPos = Handles.PositionHandle(point.position + new Vector3(0.5f, 0.5f, -0.5f), Quaternion.identity);
                point.position = new Vector3Int(
                    Mathf.RoundToInt(nextPos.x - 0.5f),
                    Mathf.RoundToInt(nextPos.y - 0.5f),
                    Mathf.RoundToInt(nextPos.z + 0.5f)
                );
            }
        }
    }
}

#endif