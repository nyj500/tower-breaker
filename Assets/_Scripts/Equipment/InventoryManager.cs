using UnityEngine;
using TowerBreaker.Data;
using TowerBreaker.Core;

namespace TowerBreaker.Equipment
{
    /// <summary>
    /// 전투 코드에서 장착 아이템 스탯을 읽을 때 사용.
    /// UI 인벤토리는 PlayerInventory가 담당.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        private PlayerInventory playerInventory;

        private void Awake()
        {
            playerInventory = PlayerInventory.Instance
                              ?? FindFirstObjectByType<PlayerInventory>();
        }

        public ItemDataSO GetEquippedWeapon()
            => playerInventory?.GetEquipped(ItemCategory.Weapon);

        public ItemDataSO GetEquippedArmor()
            => playerInventory?.GetEquipped(ItemCategory.Equipment);

        public ItemDataSO GetEquippedShoes()
            => playerInventory?.GetEquipped(ItemCategory.Shoes);

        public ItemDataSO GetEquippedAccessory()
            => playerInventory?.GetEquipped(ItemCategory.Accessory);
    }
}
