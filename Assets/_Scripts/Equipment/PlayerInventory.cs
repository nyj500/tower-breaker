using System.Collections.Generic;
using UnityEngine;
using TowerBreaker.Data;

namespace TowerBreaker.Equipment
{
    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory Instance { get; private set; }

        private readonly List<ItemDataSO> ownedItems = new();
        private readonly Dictionary<ItemCategory, ItemDataSO> equippedItems = new();

        public event System.Action OnInventoryChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void AddItem(ItemDataSO item)
        {
            if (item == null || ownedItems.Contains(item)) return;
            ownedItems.Add(item);
            OnInventoryChanged?.Invoke();
        }

        public void Equip(ItemDataSO item)
        {
            if (item == null || !ownedItems.Contains(item)) return;
            equippedItems[item.category] = item;
            OnInventoryChanged?.Invoke();
        }

        public void Unequip(ItemCategory category)
        {
            if (!equippedItems.ContainsKey(category)) return;
            equippedItems.Remove(category);
            OnInventoryChanged?.Invoke();
        }

        public void ToggleEquip(ItemDataSO item)
        {
            if (IsEquipped(item))
                Unequip(item.category);
            else
                Equip(item);
        }

        public bool IsEquipped(ItemDataSO item)
        {
            return equippedItems.TryGetValue(item.category, out var equipped) && equipped == item;
        }

        public ItemDataSO GetEquipped(ItemCategory category)
        {
            equippedItems.TryGetValue(category, out var item);
            return item;
        }

        public List<ItemDataSO> GetItemsByCategory(ItemCategory category)
        {
            return ownedItems.FindAll(i => i.category == category);
        }

        public IReadOnlyList<ItemDataSO> OwnedItems => ownedItems.AsReadOnly();
    }
}
