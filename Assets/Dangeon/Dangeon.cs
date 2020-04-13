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
            var generator = new DangeonGenerator(DangeonGeneratorData);

            for (var x = -4; x < 4; x++)
            {
                for (var y = -4; y < 4; y++)
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