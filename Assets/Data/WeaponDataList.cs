namespace Data {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "WeaponDataList", menuName = "ScriptableObjects/WeaponDataList", order = 0)]
    public class WeaponDataList : ScriptableObject {
        public WeaponData[] weapons;
    }

}