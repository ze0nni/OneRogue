namespace Dangeon
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class Dangeon : MonoBehaviour
    {
        public DangeonGeneratorData DangeonGeneratorData;

        void Start()
        {
            var random = new System.Random();
            var generator = new DangeonGenerator(DangeonGeneratorData);
            var dangeon = generator.Generate(64, 64, 1, random);           

            foreach (var r in dangeon.Rooms) {
                InsertRoom(r);
            }
        }

        private void InsertRoom(RectInt room) {
            for (var x = room.xMin; x < room.xMax; x++)
            {
                for (var y = room.yMin; y < room.yMax; y++)
                {
                    var t = DangeonGeneratorData.Tiles[Random.Range(0, DangeonGeneratorData.Tiles.Length)];
                    var tile = Instantiate(t.tilePrefab, this.transform);
                    var tileTransform = tile.GetComponent<Transform>();
                    tileTransform.position = new Vector3(
                        x * 5,
                        0,
                        y * 5
                    );
                }
            }
        }
    }


}