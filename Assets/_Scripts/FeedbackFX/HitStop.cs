using UnityEngine;
using TowerBreaker.Core;

namespace TowerBreaker.FeedbackFX
{
    /// <summary>
    /// TimeController 래퍼. 히트스톱 프리셋을 제공한다.
    /// </summary>
    public class HitStop : MonoBehaviour
    {
        [Header("Presets")]
        [SerializeField] private float lightDuration  = 0.03f;
        [SerializeField] private float lightScale     = 0.1f;
        [SerializeField] private float mediumDuration = 0.06f;
        [SerializeField] private float mediumScale    = 0.05f;
        [SerializeField] private float heavyDuration  = 0.12f;
        [SerializeField] private float heavyScale     = 0.02f;

        public void Light()
        {
            // TODO: TimeController.Instance.DoHitStop(lightDuration, lightScale)
            TimeController.Instance?.DoHitStop(lightDuration, lightScale);
        }

        public void Medium()
        {
            // TODO: TimeController.Instance.DoHitStop(mediumDuration, mediumScale)
            TimeController.Instance?.DoHitStop(mediumDuration, mediumScale);
        }

        public void Heavy()
        {
            // TODO: TimeController.Instance.DoHitStop(heavyDuration, heavyScale)
            TimeController.Instance?.DoHitStop(heavyDuration, heavyScale);
        }

        public void Custom(float duration, float slowScale = 0.05f)
        {
            // TODO: 임의 히트스톱
            TimeController.Instance?.DoHitStop(duration, slowScale);
        }
    }
}
