using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TowerBreaker.Player;
using TowerBreaker.Equipment;

namespace TowerBreaker.UI
{
    /// <summary>
    /// 스킬 버튼 3종 (무기, 방어구, 방패 스킬) 각각에 부착.
    /// 쿨다운 오버레이 및 입력 전달.
    /// </summary>
    public class SkillButtonUI : MonoBehaviour
    {
        [Header("Slot")]
        [SerializeField] private int slotIndex; // 0=무기, 1=방어구, 2=방패

        [Header("UI")]
        [SerializeField] private Button button;
        [SerializeField] private Image  cooldownOverlay;
        [SerializeField] private TextMeshProUGUI cooldownText;
        [SerializeField] private Image  skillIcon;

        private SkillExecutor skillExecutor;
        private float cooldownMax;
        private float cooldownRemaining;

        private void Awake()
        {
            skillExecutor = FindFirstObjectByType<SkillExecutor>();
            button?.onClick.AddListener(OnButtonClicked);
        }

        private void Update()
        {
            // TODO: cooldownRemaining 감소 처리 및 오버레이 갱신
            if (cooldownRemaining > 0f)
            {
                cooldownRemaining -= Time.deltaTime;
                UpdateOverlay();
            }
        }

        private void OnButtonClicked()
        {
            // TODO: 쿨다운 중이면 무시, 아니면 SkillExecutor.ExecuteSkill(slotIndex) 호출
            if (cooldownRemaining > 0f) return;
            skillExecutor?.ExecuteSkill(slotIndex);
        }

        public void StartCooldown(float duration)
        {
            // TODO: 쿨다운 시작, 버튼 비활성화
            cooldownMax       = duration;
            cooldownRemaining = duration;
            button.interactable = false;
        }

        private void UpdateOverlay()
        {
            // TODO: fillAmount = cooldownRemaining / cooldownMax
            float ratio = Mathf.Clamp01(cooldownRemaining / cooldownMax);
            if (cooldownOverlay) cooldownOverlay.fillAmount = ratio;
            if (cooldownText)    cooldownText.text = cooldownRemaining > 0f
                                    ? $"{cooldownRemaining:F1}"
                                    : "";

            if (cooldownRemaining <= 0f)
                button.interactable = true;
        }
    }
}
