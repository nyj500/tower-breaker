using System.Collections.Generic;
using UnityEngine;
using TowerBreaker.Data;

namespace TowerBreaker.Equipment
{
    [System.Serializable]
    public class LootEntry
    {
        public string itemKey;
        [Range(0f, 1f)] public float weight;

        // TODO: 실제 아이템 SO 직접 참조로 교체 가능
        public WeaponDataSO weapon;
        public ArmorDataSO  armor;
        public ShieldDataSO shield;
    }

    [CreateAssetMenu(fileName = "LootTable", menuName = "TowerBreaker/Equipment/LootTable")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private List<LootEntry> entries;

        /// <summary>
        /// 가중치 기반 랜덤 룻 항목을 반환한다.
        /// </summary>
        public LootEntry Roll()
        {
            // TODO: 총 가중치 합산 후 Random.value * totalWeight 로 항목 선택
            float totalWeight = 0f;
            foreach (var e in entries) totalWeight += e.weight;

            float roll = Random.value * totalWeight;
            float cumulative = 0f;

            foreach (var e in entries)
            {
                cumulative += e.weight;
                if (roll <= cumulative) return e;
            }

            return null;
        }

        /// <summary>
        /// 복수의 룻을 한번에 굴린다.
        /// </summary>
        public List<LootEntry> RollMultiple(int count)
        {
            // TODO: count회 Roll() 호출, 중복 허용 여부 설정 가능
            var results = new List<LootEntry>(count);
            for (int i = 0; i < count; i++)
            {
                var entry = Roll();
                if (entry != null) results.Add(entry);
            }
            return results;
        }
    }
}
