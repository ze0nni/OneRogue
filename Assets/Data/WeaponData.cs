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
        public float Reload;

        public AnimationCurve hitCurve;
    }

}