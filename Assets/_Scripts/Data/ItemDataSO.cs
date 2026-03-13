using UnityEngine;

namespace TowerBreaker.Data
{
    public enum ItemCategory { Weapon, Equipment, Shoes, Accessory }

    [CreateAssetMenu(fileName = "ItemData", menuName = "TowerBreaker/Data/ItemData")]
    public class ItemDataSO : ScriptableObject
    {
        [Header("Info")]
        public string itemName;
        public Sprite icon;
        public ItemCategory category;
        [TextArea] public string description;

        [Header("Skill")]
        public SkillDataSO skill;

        // ── Weapon ───────────────────────────────────
        [Header("Weapon (category = Weapon)")]
        public float damageMultiplier = 1f;
        public float attackSpeed = 1f;
        public int   comboSteps = 3;
        public float knockbackForce = 3f;

        // ── Equipment (Armor) ─────────────────────────
        [Header("Equipment (category = Equipment)")]
        public float defenseBonus = 0f;
        public float reflectDamageRate = 0f;
        public float damageNullifyChance = 0f;

        // ── Shoes ─────────────────────────────────────
        [Header("Shoes (category = Shoes)")]
        public float moveSpeedBonus = 0f;
        public float dashDistanceBonus = 0f;
        public float dashCooldownReduce = 0f;

        // ── Accessory ─────────────────────────────────
        [Header("Accessory (category = Accessory)")]
        public float hpBonus = 0f;
        public float skillCooldownReduce = 0f;
        public float critChance = 0f;
    }
}
