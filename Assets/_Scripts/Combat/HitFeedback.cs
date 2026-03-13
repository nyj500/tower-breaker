using UnityEngine;
using TowerBreaker.Core;
using TowerBreaker.FeedbackFX;

namespace TowerBreaker.Combat
{
    public class HitFeedback : MonoBehaviour
    {
        [Header("Presets")]
        [SerializeField] private float lightHitStopDuration   = 0.04f;
        [SerializeField] private float heavyHitStopDuration   = 0.1f;
        [SerializeField] private float lightShakeMagnitude    = 0.05f;
        [SerializeField] private float heavyShakeMagnitude    = 0.15f;
        [SerializeField] private float shakeDuration          = 0.2f;

        public void PlayLightHit(Vector3 position)
        {
            // TODO: 가벼운 타격 피드백 - 짧은 히트스톱, 약한 카메라 흔들림, 타격 VFX
            DoHitStop(lightHitStopDuration);
            DoShake(shakeDuration * 0.5f, lightShakeMagnitude);
            SpawnVFX("VFX_HitLight", position);
        }

        public void PlayHeavyHit(Vector3 position)
        {
            // TODO: 강한 타격 피드백 - 긴 히트스톱, 강한 카메라 흔들림, 타격 VFX
            DoHitStop(heavyHitStopDuration);
            DoShake(shakeDuration, heavyShakeMagnitude);
            SpawnVFX("VFX_HitHeavy", position);
        }

        public void PlaySkillHit(Vector3 position)
        {
            // TODO: 스킬 타격 피드백
        }

        private void DoHitStop(float duration)
        {
            // TODO: TimeController.Instance.DoHitStop(duration)
            TimeController.Instance?.DoHitStop(duration);
        }

        private void DoShake(float duration, float magnitude)
        {
            // TODO: CameraShaker.Instance.Shake(duration, magnitude)
            CameraShaker.Instance?.Shake(duration, magnitude);
        }

        private void SpawnVFX(string key, Vector3 position)
        {
            // TODO: ObjectPoolManager.Get(key, position, Quaternion.identity)
            ObjectPoolManager.Instance?.Get(key, position, Quaternion.identity);
        }
    }
}
