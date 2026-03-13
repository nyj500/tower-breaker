using UnityEngine;

namespace TowerBreaker.Data
{
    public enum SkillType { Melee, Projectile, AoE, Buff, Summon }

    [CreateAssetMenu(fileName = "SkillData", menuName = "TowerBreaker/Data/SkillData")]
    public class SkillDataSO : ScriptableObject
    {
        [Header("Info")]
        public string skillName;
        public Sprite icon;
        [TextArea] public string description;

        [Header("Properties")]
        public SkillType skillType;
        public float cooldown = 5f;
        public float damageMultiplier = 2f;
        public float range = 3f;
        public float duration = 0f;

        [Header("VFX / SFX")]
        public GameObject vfxPrefab;
        public string sfxKey;

        [Header("Projectile (if applicable)")]
        public GameObject projectilePrefab;
        public float projectileSpeed = 10f;
    }
}
