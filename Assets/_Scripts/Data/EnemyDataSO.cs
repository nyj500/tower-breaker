using UnityEngine;

namespace TowerBreaker.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "TowerBreaker/Data/EnemyData")]
    public class EnemyDataSO : ScriptableObject
    {
        [Header("Info")]
        public string enemyName;

        [Header("Stats")]
        public float maxHp = 50f;
        public float attack = 8f;
        public float moveSpeed = 2.5f;
        public float attackRange = 1f;
        public float attackCooldown = 1.5f;

        [Header("Knockback")]
        public float knockbackResistance = 0f;
    }
}
