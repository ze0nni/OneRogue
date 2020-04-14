namespace Dangeon
{
    using global::Dangeon.Generator;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class BSPGeneratorTest : MonoBehaviour
    {
        public Slider Width;
        public Slider Height;
        public RectTransform DangeonDisplay;

        private System.Random random = new System.Random();
        private BSPGenerator generator = new BSPGenerator(3, 15, 0.5f);

        void Start()
        {

        }

        public void Generate()
        {
            ClearDisplay();

            var rooms = generator.Generate((int)Width.value, (int)Height.value, random);
            foreach (var rect in rooms)
            {
                InsertRoom(rect);
            }

            var triangulator = new RoomsTriangulator<RectInt>(r => r);

            var links = triangulator.Generate(rooms);
            foreach (var l in links) {
                InsertLink(l);
            }
        }

        private void ClearDisplay()
        {
            foreach (Transform childTransform in DangeonDisplay.transform) Destroy(childTransform.gameObject);
        }

        private void InsertRoom(RectInt rect)
        {
            var room = new GameObject("Room", typeof(RectTransform), typeof(Image));
            room.transform.SetParent(DangeonDisplay.transform);
            room.GetComponent<Image>().color = new Color(Random.value * 0.5f + 0.5f, Random.value * 0.5f + 0.5f, Random.value * 0.5f + 0.5f);

            var transform = room.GetComponent<RectTransform>();
            transform.pivot = new Vector2(0, 1);
            transform.localPosition = new Vector3(rect.x * 10 + 1, -rect.y * 10 + 1, 0);
            transform.sizeDelta = rect.size * 10 - new Vector2Int(2, 2);
        }

        private void InsertLink((RectInt, RectInt) link) {
            var distance = Vector3.Distance(link.Item1.center, link.Item2.center);
            var angle = Mathf.Atan2(link.Item1.center.y - link.Item2.center.y, link.Item2.center.x - link.Item1.center.x);

            var lonkGo = new GameObject("Link", typeof(RectTransform), typeof(Image));
            lonkGo.transform.SetParent(DangeonDisplay.transform);

            var image = lonkGo.GetComponent<Image>();
            image.color = new Color(Random.value * 0.5f, Random.value * 0.5f, Random.value * 0.5f);

            image.transform.localPosition = new Vector3(
                link.Item1.center.x * 10,
                -link.Item1.center.y * 10,
                0
            );

            image.rectTransform.pivot = new Vector2(
                (2 / (distance * 10)) * 0.5f,
                0.5f
            );

            image.rectTransform.sizeDelta = new Vector2(distance * 10, 2);

            image.rectTransform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }
    }
}

