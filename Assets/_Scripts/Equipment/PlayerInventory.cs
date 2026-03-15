using System.Collections.Generic;
using UnityEngine;
using TowerBreaker.Data;

namespace TowerBreaker.Equipment
{
    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory Instance { get; private set; }

        private readonly List<OwnedItem> ownedItems = new();
        private readonly Dictionary<ItemCategory, OwnedItem> equippedItems = new();

        public event System.Action OnInventoryChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void AddItem(ItemDataSO item)
        {
            if (item == null) return;
            ownedItems.Add(new OwnedItem(item));
            OnInventoryChanged?.Invoke();
        }

        public void Equip(OwnedItem item)
        {
            if (item == null || !ownedItems.Contains(item)) return;
            equippedItems[item.data.category] = item;
            OnInventoryChanged?.Invoke();
        }

        public void Unequip(ItemCategory category)
        {
            if (!equippedItems.ContainsKey(category)) return;
            equippedItems.Remove(category);
            OnInventoryChanged?.Invoke();
        }

        public void ToggleEquip(OwnedItem item)
        {
            if (IsEquipped(item))
                Unequip(item.data.category);
            else
                Equip(item);
        }

        public bool IsEquipped(OwnedItem item)
        {
            return equippedItems.TryGetValue(item.data.category, out var equipped) && equipped.uid == item.uid;
        }

        public ItemDataSO GetEquipped(ItemCategory category)
        {
            equippedItems.TryGetValue(category, out var item);
            return item?.data;
        }

        public List<OwnedItem> GetItemsByCategory(ItemCategory category)
        {
            return ownedItems.FindAll(i => i.data.category == category);
        }

        public IReadOnlyList<OwnedItem> OwnedItems => ownedItems.AsReadOnly();
    }
}
