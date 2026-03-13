using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TowerBreaker.Core;
using TowerBreaker.Player;

namespace TowerBreaker.UI
{
    public class HUDController : MonoBehaviour
    {
        [Header("HP")]
        [SerializeField] private Slider hpBar;
        [SerializeField] private TextMeshProUGUI hpText;

        [Header("Floor")]
        [SerializeField] private TextMeshProUGUI floorText;

        [Header("Skill Cooldowns")]
        [SerializeField] private Image[] skillCooldownOverlays; // 0=무기, 1=방어구, 2=방패

        private PlayerController player;

        private void Start()
        {
            // TODO: EventBus 구독 (HpChangedEvent, SkillReadyEvent 등)
            player = FindFirstObjectByType<PlayerController>();
            UpdateFloorText(StageManager.Instance?.CurrentFloor ?? 0);
        }

        private void Update()
        {
            // TODO: HP 바 실시간 갱신 (EventBus로 교체 권장)
            if (player != null)
                UpdateHpBar(player.CurrentHp, player.stats.maxHp);
        }

        public void UpdateHpBar(float current, float max)
        {
            // TODO: hpBar.value = current / max
            if (hpBar != null) hpBar.value = current / max;
            if (hpText != null) hpText.text = $"{Mathf.CeilToInt(current)} / {Mathf.CeilToInt(max)}";
        }

        public void UpdateFloorText(int floor)
        {
            // TODO: "Floor {floor + 1}" 텍스트 표시
            if (floorText != null) floorText.text = $"Floor {floor + 1}";
        }

        public void UpdateSkillCooldown(int slotIndex, float ratio)
        {
            // TODO: 쿨다운 오버레이 fillAmount 설정 (ratio: 0=준비됨, 1=최대쿨다운)
            if (skillCooldownOverlays != null && slotIndex < skillCooldownOverlays.Length)
                skillCooldownOverlays[slotIndex].fillAmount = ratio;
        }
    }
}
