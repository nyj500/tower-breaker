using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using TowerBreaker.Equipment;

namespace TowerBreaker.UI
{
    public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private Image selectionHighlight;
        [SerializeField] private Image equippedHighlight;

        private OwnedItem item;

        public OwnedItem Item => item;
        public bool IsSelected { get; private set; }

        public System.Action<ItemSlotUI> OnClicked;

        private void Awake()
        {
            if (iconImage != null)
                iconImage.raycastTarget = false;
        }

        public void Setup(OwnedItem ownedItem)
        {
            Deselect();
            item = ownedItem;
            iconImage.sprite = item.data.icon;
            iconImage.enabled = item.data.icon != null;

            if (itemNameText != null)
                itemNameText.text = item.data.itemName;

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
                equippedHighlight.enabled = item != null
                    && PlayerInventory.Instance != null
                    && PlayerInventory.Instance.IsEquipped(item);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (item == null) return;
            OnClicked?.Invoke(this);
        }
    }
}
