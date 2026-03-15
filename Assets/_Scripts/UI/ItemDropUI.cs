using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TowerBreaker.Data;

namespace TowerBreaker.UI
{
    public class ItemDropUI : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemCategoryText;
        [SerializeField] private TextMeshProUGUI itemDescriptionText;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Show(ItemDataSO item)
        {
            if (item == null) return;

            if (itemIcon != null) itemIcon.sprite = item.icon;
            if (itemNameText != null) itemNameText.text = item.itemName;
            if (itemCategoryText != null) itemCategoryText.text = item.category.ToString();
            if (itemDescriptionText != null) itemDescriptionText.text = item.description;

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
