using UnityEngine;

namespace TowerBreaker.Data
{
    [CreateAssetMenu(fileName = "ArmorData", menuName = "TowerBreaker/Data/ArmorData")]
    public class ArmorDataSO : ScriptableObject
    {
        [Header("Info")]
        public string armorName;
        public Sprite icon;

        [Header("Stats")]
        [Tooltip("기본 DEF에 더해지는 보정값")]
        public float defenseBonus = 5f;

        [Header("Special Effect")]
        [Tooltip("피격 시 반사 데미지 배율 (0이면 비활성)")]
        public float reflectDamageRate = 0f;

        [Tooltip("일정 확률로 데미지 무효화 (0~1)")]
        public float damageNullifyChance = 0f;

        [Header("Skill")]
        public SkillDataSO skill;
    }
}
