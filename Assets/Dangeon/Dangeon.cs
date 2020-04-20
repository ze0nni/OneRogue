namespace Dangeon
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class Dangeon : MonoBehaviour
    {
        public Player player;
        public GameObject RoomPrefab;

        private GameObject[] rooms;
        private GameObject[] monsters;
        private GameObject[] inventoryItems;

        void Start()
        {
            player.gameObject.SetActive(false);

            StartCoroutine(PrepareDangeon());
        }

        IEnumerator PrepareDangeon() {
            Debug.Log("Start...");

            var tasks = new (string, Action<GameObject[]>)[]
            {
                ("Rooms", PrepareRooms),
                ("Monsters", result => this.monsters = result),
                ("Inventory", result => this.inventoryItems = result)
            };

            foreach (var t in tasks) {
                yield return null;
                var result = Resources.LoadAll<GameObject>(t.Item1);
                yield return null;
                t.Item2.Invoke(result);
                Debug.Log(string.Format("Loaded: {0} {1}", result.Length, t.Item1));
            }

            var generator = new DangeonGenerator();
            foreach (var generatedRoom in generator.Generate()) {
                InsertRoom(generatedRoom);
            }

            Debug.Log("Dangeon generated");

            player.gameObject.SetActive(true);
        }

        private void PrepareRooms(GameObject[] prefabs) {
            this.rooms = prefabs;
        }

        public void InsertRoom(GeneratedRoom generatedRoom) {
            Instantiate(this.rooms[0], this.transform)
                .GetComponent<Room>().Spawn(generatedRoom);
        }
    }


}