using System.Collections.Generic;
using UnityEngine;
using TowerBreaker.Data;

namespace TowerBreaker.Equipment
{
    [System.Serializable]
    public class LootEntry
    {
        public ItemDataSO item;
        [Range(0f, 1f)] public float weight;
    }

    [CreateAssetMenu(fileName = "LootTable", menuName = "TowerBreaker/Equipment/LootTable")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private List<LootEntry> entries;

        public LootEntry Roll()
        {
            float totalWeight = 0f;
            foreach (var e in entries) totalWeight += e.weight;

            float roll       = Random.value * totalWeight;
            float cumulative = 0f;

            foreach (var e in entries)
            {
                cumulative += e.weight;
                if (roll <= cumulative) return e;
            }

            return null;
        }

        public List<LootEntry> RollMultiple(int count)
        {
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
