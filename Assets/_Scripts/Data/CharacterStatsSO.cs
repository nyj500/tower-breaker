using UnityEngine;

namespace TowerBreaker.Data
{
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "TowerBreaker/Data/CharacterStats")]
    public class CharacterStatsSO : ScriptableObject
    {
        [Header("Base Stats")]
        public float maxHp = 100f;
        public float attack = 15f;
        public float defense = 5f;
        public float moveSpeed = 4f;

        [Header("Combat")]
        public float attackRange = 1.5f;
        public float attackCooldown = 0.5f;
        public float dashDistance = 3f;
        public float dashDuration = 0.2f;
        public float dashCooldown = 1.5f;

        [Header("Invincibility")]
        public float dashInvincibleDuration = 0.2f;
        public float hitStaggerDuration = 0.3f;
    }
}
