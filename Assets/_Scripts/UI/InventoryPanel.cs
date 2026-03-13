using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TowerBreaker.Equipment;
using TowerBreaker.Data;

namespace TowerBreaker.UI
{
    public class InventoryPanel : MonoBehaviour
    {
        [Header("Slot UI")]
        [SerializeField] private Image weaponIcon;
        [SerializeField] private Image armorIcon;
        [SerializeField] private Image shieldIcon;

        [Header("Buttons")]
        [SerializeField] private Button unequipWeaponBtn;
        [SerializeField] private Button unequipArmorBtn;
        [SerializeField] private Button unequipShieldBtn;

        [Header("Item List")]
        [SerializeField] private Transform itemListParent;
        [SerializeField] private GameObject itemEntryPrefab;

        private InventoryManager inventory;

        private void Awake()
        {
            // TODO: InventoryManager 참조 획득
            inventory = FindFirstObjectByType<InventoryManager>();
        }

        private void Start()
        {
            // TODO: 버튼 리스너 등록
            unequipWeaponBtn?.onClick.AddListener(() => inventory.Unequip(SlotType.Weapon));
            unequipArmorBtn?.onClick.AddListener(()  => inventory.Unequip(SlotType.Armor));
            unequipShieldBtn?.onClick.AddListener(() => inventory.Unequip(SlotType.Shield));

            // TODO: InventoryChangedEvent 구독하여 UI 갱신
            TowerBreaker.Core.EventBus.Subscribe<InventoryChangedEvent>(OnInventoryChanged);
            RefreshAll();
        }

        private void OnDestroy()
        {
            TowerBreaker.Core.EventBus.Unsubscribe<InventoryChangedEvent>(OnInventoryChanged);
        }

        private void OnInventoryChanged(InventoryChangedEvent e)
        {
            // TODO: 변경된 슬롯만 갱신
            RefreshAll();
        }

        public void RefreshAll()
        {
            // TODO: 현재 장착 장비 아이콘 갱신
            if (inventory == null) return;
            if (weaponIcon != null) weaponIcon.sprite = inventory.GetEquippedWeapon()?.icon;
            if (armorIcon  != null) armorIcon.sprite  = inventory.GetEquippedArmor()?.icon;
            if (shieldIcon != null) shieldIcon.sprite = inventory.GetEquippedShield()?.icon;

            // TODO: itemListParent 자식 정리 후 보유 아이템 목록 재생성
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
