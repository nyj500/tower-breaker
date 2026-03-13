using UnityEngine;
using TowerBreaker.Core;

namespace TowerBreaker.FeedbackFX
{
    /// <summary>
    /// CameraShaker 래퍼. 강도/지속시간 프리셋을 제공한다.
    /// </summary>
    public class ScreenShake : MonoBehaviour
    {
        [Header("Presets")]
        [SerializeField] private float lightDuration   = 0.1f;
        [SerializeField] private float lightMagnitude  = 0.05f;
        [SerializeField] private float mediumDuration  = 0.2f;
        [SerializeField] private float mediumMagnitude = 0.12f;
        [SerializeField] private float heavyDuration   = 0.35f;
        [SerializeField] private float heavyMagnitude  = 0.25f;

        public void Light()
        {
            // TODO: CameraShaker.Instance.Shake(lightDuration, lightMagnitude)
            CameraShaker.Instance?.Shake(lightDuration, lightMagnitude);
        }

        public void Medium()
        {
            // TODO: CameraShaker.Instance.Shake(mediumDuration, mediumMagnitude)
            CameraShaker.Instance?.Shake(mediumDuration, mediumMagnitude);
        }

        public void Heavy()
        {
            // TODO: CameraShaker.Instance.Shake(heavyDuration, heavyMagnitude)
            CameraShaker.Instance?.Shake(heavyDuration, heavyMagnitude);
        }

        public void Custom(float duration, float magnitude)
        {
            // TODO: 임의 강도 흔들림
            CameraShaker.Instance?.Shake(duration, magnitude);
        }
    }
}
