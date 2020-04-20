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

        private GameObject[] monsters;
        private GameObject[] inventoryItems;

        void Start()
        {
            player.gameObject.SetActive(false);

            StartCoroutine(PrepareDangeon());
        }

        IEnumerator PrepareDangeon() {
            Debug.Log("Start...");

            yield return new WaitForEndOfFrame();
            this.monsters = Resources.LoadAll<GameObject>("Monsters");
            Debug.Log(string.Format("{0} Monsters loaded", monsters.Length));

            yield return new WaitForEndOfFrame();
            this.inventoryItems = Resources.LoadAll<GameObject>("Inventory");
            Debug.Log(string.Format("{0} Inventory items loaded", inventoryItems.Length));

            yield return new WaitForEndOfFrame();
            var random = new System.Random();
            var generator = new DangeonGenerator(DangeonGeneratorData);
            var dangeon = generator.Generate(24, 24, 1, random);
            Debug.Log("Dangeon generated");

            yield return new WaitForEndOfFrame();
            foreach (var r in dangeon.Rooms)
            {
                InsertRoom(r);
            }

            var start = dangeon.Rooms[0];
            player.transform.position = new Vector3(
                start.center.x * 5,
                5,
                start.center.y * 5
            );

            player.gameObject.SetActive(true);
        }

        private void InsertRoom(RectInt room) {
            Instantiate(RoomPrefab, this.transform)
                .GetComponent<RoomLegasy>()
                .Create(DangeonGeneratorData, room);

            var monstersCount = Random.Range(3, 7);
            for (var i = 0; i < monstersCount; i++)
            {
                var monsterData = monsters[Random.Range(0, monsters.Length)];

                var monster = Instantiate(monsterData, this.transform);
                monster.transform.localPosition = new Vector3(
                    (room.x + Random.Range(0, room.width)) * 5 + 2.5f,
                    1,
                    (room.y + Random.Range(0, room.height)) * 5 + 2.5f
                );
            }

            var invCount = Random.Range(3, 7);
            for (var i = 0; i < invCount; i++)
            {
                var invData = inventoryItems[Random.Range(0, inventoryItems.Length)];

                var inv = Instantiate(invData, this.transform);
                inv.transform.localPosition = new Vector3(
                    (room.x + Random.Range(0, room.width)) * 5 + 2.5f,
                    1,
                    (room.y + Random.Range(0, room.height)) * 5 + 2.5f
                );
            }
        }
    }


}