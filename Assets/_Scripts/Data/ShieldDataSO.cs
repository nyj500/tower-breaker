using UnityEngine;

namespace TowerBreaker.Data
{
    [CreateAssetMenu(fileName = "ShieldData", menuName = "TowerBreaker/Data/ShieldData")]
    public class ShieldDataSO : ScriptableObject
    {
        [Header("Info")]
        public string shieldName;
        public Sprite icon;

        [Header("Block")]
        [Tooltip("블록 쿨다운 (초)")]
        public float blockCooldown = 3f;

        [Tooltip("블록 성공 시 피해 감소율 (0~1)")]
        public float blockDamageReduction = 0.8f;

        [Tooltip("블록 성공 유효 시간 (초)")]
        public float blockWindowDuration = 0.3f;

        [Header("Knockback")]
        [Tooltip("넉백 저항 배율 (0이면 완전 저항)")]
        public float knockbackResistance = 0.5f;

        [Header("Parry")]
        [Tooltip("패리 성공 시 반격 배율 (0이면 비활성)")]
        public float parryCounterMultiplier = 0f;

        [Header("Skill")]
        public SkillDataSO skill;
    }
}
