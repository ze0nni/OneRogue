namespace Data
{

    using UnityEngine;

    [System.Serializable]
    public class WeaponData
    {
        public string Name;
        public Sprite Image;

        public float Demage;
        public float Range;

        public float Prepare;
        public float Release;
        public float Reload;

        public AnimationCurve PrepareCurveYOffset;
        public AnimationCurve PrepareCurveZRotation;
    }

}