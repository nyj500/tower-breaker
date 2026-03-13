using UnityEngine;
using TowerBreaker.Data;

namespace TowerBreaker.Equipment
{
    [System.Serializable]
    public class EquipmentSlot
    {
        public ItemCategory slotCategory;
        public ItemDataSO equippedItem;

        public bool IsEmpty() => equippedItem == null;

        public void Clear() => equippedItem = null;

        public string GetItemName() => equippedItem?.itemName ?? "Empty";
    }
}
