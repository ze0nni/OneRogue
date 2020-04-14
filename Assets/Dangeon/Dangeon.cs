namespace Dangeon
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class Dangeon : MonoBehaviour
    {
        public Player player;
        public DangeonGeneratorData DangeonGeneratorData;
        public GameObject RoomPrefab;

        void Start()
        {
            var random = new System.Random();
            var generator = new DangeonGenerator(DangeonGeneratorData);
            var dangeon = generator.Generate(24, 24, 1, random);           

            foreach (var r in dangeon.Rooms) {
                InsertRoom(r);
            }

            var start = dangeon.Rooms[0];
            player.transform.position = new Vector3(
                start.center.x * 5,
                5,
                start.center.y * 5
            );
        }

        private void InsertRoom(RectInt room) {
            Instantiate(RoomPrefab, this.transform)
                .GetComponent<Room>()
                .Create(DangeonGeneratorData, room);

            var monstersCount = Random.Range(3, 7);
            for (var i = 0; i < monstersCount; i++) {
                var monsterData = DangeonGeneratorData.Monsters[Random.Range(0, DangeonGeneratorData.Monsters.Length)];

                var monster = Instantiate(monsterData.MonsterPrefab, this.transform);
                monster.transform.localPosition = new Vector3(
                    (room.x + Random.Range(0, room.width)) * 5 + 2.5f,
                    1,
                    (room.y + Random.Range(0, room.height)) * 5 + 2.5f
                );
            }
        }
    }


}