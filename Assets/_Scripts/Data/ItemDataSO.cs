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

        public float damageMultiplier = 1f;
        public float knockbackForce = 3f;
        public float hpBonus = 0f;
        public float skillCooldownReduce = 0f;
        public float critChance = 0f;
    }
}
