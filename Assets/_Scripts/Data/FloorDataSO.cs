using UnityEngine;
using System.Collections.Generic;

namespace TowerBreaker.Data
{
    [CreateAssetMenu(fileName = "FloorData", menuName = "TowerBreaker/Data/FloorData")]
    public class FloorDataSO : ScriptableObject
    {
        [System.Serializable]
        public class EnemyEntry
        {
            public GameObject prefab;
            public int count = 1;
        }

        public List<EnemyEntry> enemies;
    }
}
