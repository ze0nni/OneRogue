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
        public AnimationCurve PrepareCurveYOffset;
        public AnimationCurve PrepareCurveZRotation;

        public float Reload;
        public AnimationCurve ReloadCurveYOffset;
        public AnimationCurve ReloadCurveZRotation;
    }

}