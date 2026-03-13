using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using TowerBreaker.Data;
using TowerBreaker.Equipment;

namespace TowerBreaker.UI
{
    public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private Image selectionHighlight; // 1번 클릭 - 선택 표시
        [SerializeField] private Image equippedHighlight;  // 착용 중 표시

        private ItemDataSO item;

        public ItemDataSO Item => item;
        public bool IsSelected { get; private set; }

        // 클릭 이벤트 - InventoryPanel이 구독
        public System.Action<ItemSlotUI> OnClicked;

        private void Awake()
        {
            if (iconImage != null)
                iconImage.raycastTarget = false;
        }

        public void Setup(ItemDataSO itemData)
        {
            Deselect();
            item = itemData;
            iconImage.sprite = item.icon;
            iconImage.enabled = item.icon != null;

            if (itemNameText != null)
                itemNameText.text = item.itemName;

            RefreshHighlight();
        }

        public void Clear()
        {
            item = null;
            iconImage.sprite = null;
            iconImage.enabled = false;

            if (itemNameText != null)
                itemNameText.text = string.Empty;

            Deselect();

            if (equippedHighlight != null)
                equippedHighlight.enabled = false;
        }

        public void Select()
        {
            IsSelected = true;
            if (selectionHighlight != null)
                selectionHighlight.enabled = true;
        }

        public void Deselect()
        {
            IsSelected = false;
            if (selectionHighlight != null)
                selectionHighlight.enabled = false;
        }

        public void RefreshHighlight()
        {
            if (equippedHighlight != null)
                equippedHighlight.enabled = PlayerInventory.Instance != null
                                            && PlayerInventory.Instance.IsEquipped(item);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (item == null) return;
            OnClicked?.Invoke(this);
        }
    }
}
