using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TowerBreaker.Data;
using TowerBreaker.Equipment;

namespace TowerBreaker.UI
{
    public class InventoryPanel : MonoBehaviour
    {
        [Header("Tabs")]
        [SerializeField] private Button weaponTabBtn;
        [SerializeField] private Button equipmentTabBtn;
        [SerializeField] private Button shoesTabBtn;
        [SerializeField] private Button accessoryTabBtn;

        [Header("Tab Objects (Tab 오브젝트 자체를 할당)")]
        [SerializeField] private GameObject weaponTab;
        [SerializeField] private GameObject equipmentTab;
        [SerializeField] private GameObject shoesTab;
        [SerializeField] private GameObject accessoryTab;

        [Header("Tab Active Colors")]
        [SerializeField] private Color tabActiveColor = Color.white;
        [SerializeField] private Color tabInactiveColor = new Color(0.6f, 0.6f, 0.6f, 1f);

        [Header("Inventory")]
        [SerializeField] private Transform inventorySlotParent;

        [Header("Equipment Slots")]
        [SerializeField] private EquipmentSlotUI weaponSlotUI;
        [SerializeField] private EquipmentSlotUI equipmentSlotUI;
        [SerializeField] private EquipmentSlotUI shoesSlotUI;
        [SerializeField] private EquipmentSlotUI accessorySlotUI;

        [Header("Bottom UI")]
        [SerializeField] private Button exitButton;

        private ItemCategory currentTab = ItemCategory.Weapon;
        private ItemSlotUI[] slots;
        private ItemSlotUI selectedSlot;

        private Button[] tabButtons;
        private GameObject[] tabs;

        private void Awake()
        {
            slots = inventorySlotParent.GetComponentsInChildren<ItemSlotUI>(true);
            tabButtons = new Button[] { weaponTabBtn, equipmentTabBtn, shoesTabBtn, accessoryTabBtn };
            tabs = new GameObject[] { weaponTab, equipmentTab, shoesTab, accessoryTab };
        }

        private void Start()
        {
            weaponTabBtn.onClick.AddListener(()    => OnTabClicked(ItemCategory.Weapon));
            equipmentTabBtn.onClick.AddListener(() => OnTabClicked(ItemCategory.Equipment));
            shoesTabBtn.onClick.AddListener(()     => OnTabClicked(ItemCategory.Shoes));
            accessoryTabBtn.onClick.AddListener(() => OnTabClicked(ItemCategory.Accessory));

            exitButton.onClick.AddListener(OnExitClicked);

            foreach (var slot in slots)
                slot.OnClicked += OnSlotClicked;

            if (PlayerInventory.Instance != null)
                PlayerInventory.Instance.OnInventoryChanged += OnInventoryChanged;

            Refresh();
        }

        private void OnDestroy()
        {
            if (PlayerInventory.Instance != null)
                PlayerInventory.Instance.OnInventoryChanged -= OnInventoryChanged;
        }

        private void OnEnable()
        {
            Refresh();
        }

        // ── 인벤토리 열기 ─────────────────────────────

        public void Show()
        {
            gameObject.SetActive(true);
        }

        // ── 슬롯 클릭 처리 ───────────────────────────

        private void OnSlotClicked(ItemSlotUI clickedSlot)
        {
            if (selectedSlot == clickedSlot)
            {
                PlayerInventory.Instance?.ToggleEquip(clickedSlot.Item);
                clickedSlot.Deselect();
                selectedSlot = null;
                RefreshEquipmentSlots();
                return;
            }

            selectedSlot?.Deselect();
            selectedSlot = clickedSlot;
            selectedSlot.Select();
        }

        // ── 탭 전환 ───────────────────────────────────

        private void OnTabClicked(ItemCategory category)
        {
            currentTab = category;
            selectedSlot?.Deselect();
            selectedSlot = null;
            RefreshInventorySlots();
            RefreshTabColors();
        }

        private void OnExitClicked()
        {
            selectedSlot?.Deselect();
            selectedSlot = null;
            gameObject.SetActive(false);
        }

        // ── 갱신 ──────────────────────────────────────

        private void Refresh()
        {
            RefreshInventorySlots();
            RefreshEquipmentSlots();
            RefreshTabColors();
        }

        private void OnInventoryChanged()
        {
            Refresh();
        }

        private void RefreshInventorySlots()
        {
            List<ItemDataSO> items = PlayerInventory.Instance != null
                ? PlayerInventory.Instance.GetItemsByCategory(currentTab)
                : new List<ItemDataSO>();

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < items.Count)
                    slots[i].Setup(items[i]);
                else
                    slots[i].Clear();
            }
        }

        private void RefreshEquipmentSlots()
        {
            weaponSlotUI?.Refresh();
            equipmentSlotUI?.Refresh();
            shoesSlotUI?.Refresh();
            accessorySlotUI?.Refresh();
        }

        private void RefreshTabColors()
        {
            ItemCategory[] categories = { ItemCategory.Weapon, ItemCategory.Equipment,
                                          ItemCategory.Shoes,  ItemCategory.Accessory };

            for (int i = 0; i < tabs.Length; i++)
            {
                if (tabs[i] == null) continue;
                Color color = categories[i] == currentTab ? tabActiveColor : tabInactiveColor;
                foreach (var img in tabs[i].GetComponentsInChildren<Image>())
                {
                    if (img.GetComponent<Button>() != null) continue;
                    img.color = color;
                }
            }
        }
    }
}
