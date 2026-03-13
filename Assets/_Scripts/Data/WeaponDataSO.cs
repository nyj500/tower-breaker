using UnityEngine;

namespace TowerBreaker.Data
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "TowerBreaker/Data/WeaponData")]
    public class WeaponDataSO : ScriptableObject
    {
        [Header("Info")]
        public string weaponName;
        public Sprite icon;

        [Header("Stats")]
        public float damageMultiplier = 1f;
        public float attackSpeed = 1f;
        public int comboSteps = 3;

        [Header("Skill")]
        public SkillDataSO skill;

        [Header("Combo Timings")]
        [Tooltip("각 콤보 단계별 활성 판정 시간 (초)")]
        public float[] comboHitWindows;

        [Header("Knockback")]
        public float knockbackForce = 3f;
    }
}
