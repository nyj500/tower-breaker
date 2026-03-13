using System.Collections.Generic;
using UnityEngine;
using TowerBreaker.Data;
using TowerBreaker.Core;

namespace TowerBreaker.Equipment
{
    public class InventoryManager : MonoBehaviour
    {
        [Header("Equipment Slots")]
        [SerializeField] private EquipmentSlot weaponSlot = new() { slotType = SlotType.Weapon };
        [SerializeField] private EquipmentSlot armorSlot  = new() { slotType = SlotType.Armor  };
        [SerializeField] private EquipmentSlot shieldSlot = new() { slotType = SlotType.Shield };

        // 보유 아이템 목록 (키: SO 인스턴스)
        private List<WeaponDataSO> ownedWeapons = new();
        private List<ArmorDataSO>  ownedArmors  = new();
        private List<ShieldDataSO> ownedShields = new();

        public void EquipWeapon(WeaponDataSO weapon)
        {
            // TODO: 기존 장착 무기 해제 후 새 무기 장착, InventoryChangedEvent 발행
            weaponSlot.equippedWeapon = weapon;
            EventBus.Publish(new InventoryChangedEvent { SlotType = SlotType.Weapon });
        }

        public void EquipArmor(ArmorDataSO armor)
        {
            // TODO: 기존 방어구 해제 후 새 방어구 장착
            armorSlot.equippedArmor = armor;
            EventBus.Publish(new InventoryChangedEvent { SlotType = SlotType.Armor });
        }

        public void EquipShield(ShieldDataSO shield)
        {
            // TODO: 기존 방패 해제 후 새 방패 장착
            shieldSlot.equippedShield = shield;
            EventBus.Publish(new InventoryChangedEvent { SlotType = SlotType.Shield });
        }

        public void Unequip(SlotType slot)
        {
            // TODO: 슬롯 타입에 따라 해당 슬롯 Clear()
            switch (slot)
            {
                case SlotType.Weapon: weaponSlot.Clear(); break;
                case SlotType.Armor:  armorSlot.Clear();  break;
                case SlotType.Shield: shieldSlot.Clear(); break;
            }
            EventBus.Publish(new InventoryChangedEvent { SlotType = slot });
        }

        public void AddItem(WeaponDataSO w) => ownedWeapons.Add(w);
        public void AddItem(ArmorDataSO  a) => ownedArmors.Add(a);
        public void AddItem(ShieldDataSO s) => ownedShields.Add(s);

        public WeaponDataSO GetEquippedWeapon() => weaponSlot.equippedWeapon;
        public ArmorDataSO  GetEquippedArmor()  => armorSlot.equippedArmor;
        public ShieldDataSO GetEquippedShield() => shieldSlot.equippedShield;

        public IReadOnlyList<WeaponDataSO> OwnedWeapons => ownedWeapons.AsReadOnly();
        public IReadOnlyList<ArmorDataSO>  OwnedArmors  => ownedArmors.AsReadOnly();
        public IReadOnlyList<ShieldDataSO> OwnedShields => ownedShields.AsReadOnly();
    }

    public struct InventoryChangedEvent
    {
        public SlotType SlotType;
    }
}
