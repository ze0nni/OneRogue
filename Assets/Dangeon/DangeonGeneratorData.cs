namespace Dangeon {
    using Monsters;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "DangeonGeneratorData", menuName = "ScriptableObjects/DangeonGeneratorData", order = 2)]
    public class DangeonGeneratorData : ScriptableObject
    {
        [System.Serializable] public class TileData {
            public GameObject tilePrefab;
        }

        public TileData[] Tiles;
    }

}