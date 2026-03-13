using UnityEngine;

namespace TowerBreaker.Combat
{
    public static class DamageCalculator
    {
        [Range(0.8f, 1.2f)]
        private const float RandomMin = 0.85f;
        private const float RandomMax = 1.15f;

        /// <summary>
        /// 최종 데미지 = (atk * multiplier - def) * 랜덤보정 (최소 1).
        /// </summary>
        public static float Calculate(float atk, float def, float multiplier = 1f)
        {
            // TODO: 크리티컬, 속성 상성 등 추가 보정 확장 가능
            float random = Random.Range(RandomMin, RandomMax);
            float damage = (atk * multiplier - def) * random;
            return Mathf.Max(1f, damage);
        }

        /// <summary>
        /// 블록 데미지 = 원래 데미지 * (1 - blockReduction).
        /// </summary>
        public static float CalculateBlocked(float rawDamage, float blockReduction)
        {
            // TODO: 패리 성공 시 0 반환 등 확장
            return Mathf.Max(1f, rawDamage * (1f - blockReduction));
        }
    }
}
