using UnityEngine;
using TowerBreaker.Core;

namespace TowerBreaker.Equipment
{
    public class ItemDropper : MonoBehaviour
    {
        [Header("Drop Config")]
        [SerializeField] private LootTable lootTable;
        [SerializeField] private GameObject itemPickupPrefab;
        [SerializeField] private float dropChance = 0.3f;

        /// <summary>
        /// 적 사망 시 호출. 확률에 따라 아이템을 드롭한다.
        /// </summary>
        public void TryDrop(Vector3 position)
        {
            // TODO: dropChance 확률 체크
            if (Random.value > dropChance) return;

            Drop(position);
        }

        private void Drop(Vector3 position)
        {
            // TODO: lootTable.Roll()로 항목 결정
            LootEntry entry = lootTable?.Roll();
            if (entry == null) return;

            // TODO: ObjectPoolManager.Get("ItemPickup", position, ...) 또는 Instantiate
            //       드롭된 아이템 GO에 LootEntry 데이터 주입
            var obj = ObjectPoolManager.Instance != null
                ? ObjectPoolManager.Instance.Get("ItemPickup", position, Quaternion.identity)
                : Instantiate(itemPickupPrefab, position, Quaternion.identity);

            // TODO: obj.GetComponent<ItemPickup>().Setup(entry);
        }
    }
}
