namespace Dangeon
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DangeonGenerator
    {
        public GeneratedRoom[] Generate() {
            return new GeneratedRoom[]{
                new GeneratedRoom(new Vector2Int(0, 0)),
                new GeneratedRoom(new Vector2Int(1, 0)),
                new GeneratedRoom(new Vector2Int(2, 0)),
                new GeneratedRoom(new Vector2Int(3, 0)),
                new GeneratedRoom(new Vector2Int(0, 1)),
                new GeneratedRoom(new Vector2Int(1, 1)),
                new GeneratedRoom(new Vector2Int(2, 1)),
                new GeneratedRoom(new Vector2Int(3, 1))
            };
        }
    }

    public class GeneratedRoom {
        public readonly Vector2Int position;

        public GeneratedRoom(Vector2Int position)
        {
            this.position = position;
        }
    }
}