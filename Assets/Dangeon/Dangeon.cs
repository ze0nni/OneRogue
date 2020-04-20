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
                ("Rooms", result => this.rooms = result),
                ("Monsters", result => this.monsters = result),
                ("Inventory", result => this.inventoryItems = result)
            };

            foreach (var t in tasks) {
                yield return new WaitForEndOfFrame();
                var result = Resources.LoadAll<GameObject>(t.Item1);
                t.Item2.Invoke(result);
                Debug.Log(string.Format("Loaded: {0} {1}", result.Length, t.Item1));
            }

            
            Debug.Log("Dangeon generated");

            player.gameObject.SetActive(true);
        }
    }


}