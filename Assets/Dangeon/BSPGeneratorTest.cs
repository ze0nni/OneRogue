namespace Dangeon
{
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
        private BSPGenerator generator = new BSPGenerator(5, 30, 1, 0.5f);

        void Start()
        {

        }

        public void Generate() {
            ClearDisplay();
            foreach (var rect in generator.Generate((int)Width.value, (int)Height.value, random)) {
                InsertRoom(rect);
            }
        }

        private void ClearDisplay()  {
            foreach (Transform childTransform in DangeonDisplay.transform) Destroy(childTransform.gameObject);
        }

        private void InsertRoom(RectInt rect) {
            var room = new GameObject("Room", typeof(RectTransform), typeof(Image));
            room.transform.SetParent(DangeonDisplay.transform);
            room.GetComponent<Image>().color = Color.black;

            var transform = room.GetComponent<RectTransform>();
            transform.pivot = new Vector2(0, 1);
            transform.localPosition = new Vector3(rect.x * 10, -rect.y * 10, 0);
            transform.sizeDelta = rect.size * 10;
        }
    }

}