using UnityEngine;
using TowerBreaker.Core;

namespace TowerBreaker.Combat
{
    public class HitFeedback : MonoBehaviour
    {
        [Header("VFX Objects (자식으로 달아둔 것)")]
        [SerializeField] private GameObject vfxHitLight;
        [SerializeField] private GameObject vfxHitHeavy;

        [Header("Presets")]
        [SerializeField] private float lightHitStopDuration = 0.05f;
        [SerializeField] private float heavyHitStopDuration = 0.1f;
        [SerializeField] private float lightShakeMagnitude = 0.10f;
        [SerializeField] private float heavyShakeMagnitude = 0.15f;
        [SerializeField] private float shakeDuration = 0.2f;

        public void PlayLightHit(Vector3 position)
        {
            DoHitStop(lightHitStopDuration);
            DoShake(shakeDuration * 0.5f, lightShakeMagnitude);
            PlayVFX(vfxHitLight);
        }

        public void PlayHeavyHit(Vector3 position)
        {
            DoHitStop(heavyHitStopDuration);
            DoShake(shakeDuration, heavyShakeMagnitude);
            PlayVFX(vfxHitHeavy);
        }

        private void DoHitStop(float duration)
        {
            TimeController.Instance?.DoHitStop(duration);
        }

        private void DoShake(float duration, float magnitude)
        {
            CameraShaker.Instance?.Shake(duration, magnitude);
        }

        private void PlayVFX(GameObject vfx)
        {
            if (vfx == null) return;
            vfx.SetActive(false);
            vfx.SetActive(true);
        }
    }
}
