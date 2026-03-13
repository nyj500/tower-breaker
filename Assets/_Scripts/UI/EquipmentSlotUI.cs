using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TowerBreaker.Data;
using TowerBreaker.Equipment;

namespace TowerBreaker.UI
{
    public class EquipmentSlotUI : MonoBehaviour
    {
        [Header("슬롯 카테고리")]
        public ItemCategory slotCategory;

        [Header("UI References")]
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private GameObject emptyIndicator;

        public void Refresh()
        {
            if (PlayerInventory.Instance == null) return;

            ItemDataSO equipped = PlayerInventory.Instance.GetEquipped(slotCategory);
            bool hasItem = equipped != null;

            if (iconImage != null)
            {
                iconImage.enabled = hasItem;
                if (hasItem) iconImage.sprite = equipped.icon;
            }

            if (itemNameText != null)
                itemNameText.text = hasItem ? equipped.itemName : string.Empty;

            if (emptyIndicator != null)
                emptyIndicator.SetActive(!hasItem);
        }
    }
}
