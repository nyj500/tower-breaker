using System.Collections;
using UnityEngine;
using TMPro;
using TowerBreaker.Core;

namespace TowerBreaker.FeedbackFX
{
    /// <summary>
    /// 데미지 숫자 팝업. ObjectPoolManager에서 꺼내 사용.
    /// </summary>
    public class DamagePopup : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshPro label;

        [Header("Animation")]
        [SerializeField] private float floatSpeed    = 1.5f;
        [SerializeField] private float fadeDuration  = 0.5f;
        [SerializeField] private float lifetime      = 0.8f;

        [Header("Colors")]
        [SerializeField] private Color normalColor   = Color.white;
        [SerializeField] private Color criticalColor = Color.yellow;

        [SerializeField] private GameObject prefab; // 자기 자신의 프리팹 참조

        /// <summary>
        /// 팝업을 초기화하고 플로팅 애니메이션을 시작한다.
        /// </summary>
        public void Setup(float damage, bool isCritical = false)
        {
            // TODO: 텍스트 설정 (크리티컬이면 "!!" 접미사 및 색상 변경)
            label.text  = isCritical ? $"{Mathf.CeilToInt(damage)}!!" : $"{Mathf.CeilToInt(damage)}";
            label.color = isCritical ? criticalColor : normalColor;
            StartCoroutine(FloatAndFade());
        }

        private IEnumerator FloatAndFade()
        {
            // TODO: lifetime 동안 위로 부유, 마지막 fadeDuration 동안 알파 감소
            float elapsed = 0f;
            Vector3 startPos = transform.position;

            while (elapsed < lifetime)
            {
                elapsed += Time.unscaledDeltaTime;
                transform.position = startPos + Vector3.up * (floatSpeed * elapsed);

                float fadeStart = lifetime - fadeDuration;
                if (elapsed > fadeStart)
                {
                    float alpha = 1f - (elapsed - fadeStart) / fadeDuration;
                    Color c = label.color;
                    c.a = alpha;
                    label.color = c;
                }
                yield return null;
            }

            // TODO: ObjectPoolManager에 반환
            ObjectPoolManager.Instance?.Return(prefab, gameObject);
        }
    }
}
