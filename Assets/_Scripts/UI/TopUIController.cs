using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TowerBreaker.Player;
using TowerBreaker.Stage;
using TowerBreaker.Core;

namespace TowerBreaker.UI
{
    public class TopUIController : MonoBehaviour
    {
        [Header("HP Hearts")]
        [SerializeField] private Image[] hpImages;   // 3개, index 0=왼쪽, 2=오른쪽
        [SerializeField] private Sprite hpFull;
        [SerializeField] private Sprite hpEmpty;

        [Header("Floor")]
        [SerializeField] private TextMeshProUGUI floorText;

        // [Header("Skill Cooldowns")]
        // [SerializeField] private Image[] skillCooldownOverlays; // 0=무기, 1=방어구, 2=방패

        private void Start()
        {
            EventBus.Subscribe<PlayerHpChangedEvent>(OnHpChanged);
            UpdateFloorText(FloorManager.Instance?.CurrentFloor ?? 0);

            var player = FindFirstObjectByType<PlayerController>();
            if (player != null)
                UpdateHpImages(player.CurrentHp);
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe<PlayerHpChangedEvent>(OnHpChanged);
        }

        private void OnHpChanged(PlayerHpChangedEvent e)
        {
            UpdateHpImages(e.CurrentHp);
        }

        private void UpdateHpImages(int current)
        {
            if (hpImages == null) return;
            int len = hpImages.Length;
            for (int i = 0; i < len; i++)
            {
                if (hpImages[i] == null) continue;
                // index 0=왼쪽, len-1=오른쪽. 오른쪽부터 비워지도록 역순 비교
                hpImages[i].sprite = (len - i) <= current ? hpFull : hpEmpty;
            }
        }

        public void UpdateFloorText(int floor)
        {
            if (floorText != null) floorText.text = $"Floor {floor + 1}";
        }

        // public void UpdateSkillCooldown(int slotIndex, float ratio)
        // {
        //     if (skillCooldownOverlays != null && slotIndex < skillCooldownOverlays.Length)
        //         skillCooldownOverlays[slotIndex].fillAmount = ratio;
        // }
    }
}
