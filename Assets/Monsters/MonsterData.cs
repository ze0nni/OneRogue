namespace Monsters
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/MonsterData", order = 3)]
    public class MonsterData : ScriptableObject
    {
        public GameObject MonsterPrefab;
    }

}