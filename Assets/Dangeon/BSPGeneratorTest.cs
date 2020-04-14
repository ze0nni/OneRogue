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
            foreach (var rect in generator.Generate((int)Width.value, (int)Height.value, random))
            {
                Debug.Log(rect.ToString());
                InsertRoom(rect);
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
    }
}

