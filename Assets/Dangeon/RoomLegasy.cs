namespace Dangeon
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RoomLegasy : MonoBehaviour
    {
        private static readonly float Unit = 5f;

        public void Create(DangeonGeneratorData dangeonGeneratorData, RectInt room)
        {
            this.transform.localPosition = new Vector3(
                room.x * Unit,
                0,
                room.y * Unit
            );

            var collider = GetComponent<BoxCollider>();
            collider.center = new Vector3(
                room.width * Unit * 0.5f,
                Unit * 0.5f,
                room.height * Unit * 0.5f
            );

            collider.size = new Vector3(
                room.width * Unit,
                Unit,
                room.height * Unit
            );

            for (var x = 0; x < room.width; x++)
            {
                for (var y = 0; y < room.height; y++)
                {
                    var t = dangeonGeneratorData.Tiles[Random.Range(0, dangeonGeneratorData.Tiles.Length)];
                    var tile = Instantiate(t.tilePrefab, this.transform);
                    var tileTransform = tile.GetComponent<Transform>();
                    tileTransform.localPosition = new Vector3(
                        x * 5,
                        0,
                        y * 5
                    );
                }
            }
        }
    }

}