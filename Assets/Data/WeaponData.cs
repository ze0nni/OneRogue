namespace Data
{
    using EditorTools;
    using UnityEngine;

    [System.Serializable]
    public class WeaponData
    {
        public string Name;
        public Sprite Image;

        public float Mass = 1f;
        public float Demage;
        public float Range;

        public float Prepare;
        public float Release;
        public float Reload;

        [CurveAttribute(0, -1, 3, 2, true)]
        public AnimationCurve PrepareCurveYOffset = AnimationCurve.Linear(0, 0, 3, 0);

        [CurveAttribute(0, -1, 3, 2, true)]
        public AnimationCurve PrepareCurveZRotation = AnimationCurve.Linear(0, 0, 3, 0);
    }

}