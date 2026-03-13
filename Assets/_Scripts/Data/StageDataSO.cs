using System.Collections.Generic;
using UnityEngine;

namespace TowerBreaker.Data
{
    [System.Serializable]
    public class WaveData
    {
        public EnemyDataSO[] enemyTypes;
        public int[] enemyCountPerType;
        [Tooltip("다음 웨이브 시작 전 대기 시간 (초)")]
        public float delayBeforeWave = 1f;
    }

    [CreateAssetMenu(fileName = "StageData", menuName = "TowerBreaker/Data/StageData")]
    public class StageDataSO : ScriptableObject
    {
        [Header("Floor Info")]
        public int floorNumber;
        public string floorName;
        public bool isBossFloor;

        [Header("Waves")]
        public List<WaveData> waves;
        public int waveCount => waves?.Count ?? 0;

        [Header("Boss (optional)")]
        public GameObject bossPrefab;

        [Header("Rewards")]
        [Tooltip("클리어 시 골드 지급량")]
        public int goldReward;
    }
}
