using UnityEngine;
using TowerBreaker.Data;

namespace TowerBreaker.Equipment
{
    public enum SlotType { Weapon, Armor, Shield }

    [System.Serializable]
    public class EquipmentSlot
    {
        public SlotType slotType;

        // 슬롯별로 하나의 ScriptableObject 참조 (다형성 없이 타입별 필드 사용)
        public WeaponDataSO equippedWeapon;
        public ArmorDataSO  equippedArmor;
        public ShieldDataSO equippedShield;

        public bool IsEmpty()
        {
            return slotType switch
            {
                SlotType.Weapon => equippedWeapon == null,
                SlotType.Armor  => equippedArmor  == null,
                SlotType.Shield => equippedShield == null,
                _               => true
            };
        }

        public void Clear()
        {
            // TODO: 장착 해제 시 해당 슬롯 SO 참조를 null로 초기화
            equippedWeapon = null;
            equippedArmor  = null;
            equippedShield = null;
        }

        public string GetItemName()
        {
            return slotType switch
            {
                SlotType.Weapon => equippedWeapon?.weaponName ?? "Empty",
                SlotType.Armor  => equippedArmor?.armorName  ?? "Empty",
                SlotType.Shield => equippedShield?.shieldName ?? "Empty",
                _               => "Empty"
            };
        }
    }
}
