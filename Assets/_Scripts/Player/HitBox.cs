using UnityEngine;
using TowerBreaker.Enemy;
using TowerBreaker.Combat;

namespace TowerBreaker.Player
{
    public class HitBox : MonoBehaviour
    {
        public enum HitType
        {
            Attack,
            Skill1,
            Skill2,
            Skill3,
            Block
        }

        [SerializeField] private HitType type;

        private PlayerCombat combat;

        private void Awake()
        {
            combat = GetComponentInParent<PlayerCombat>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var enemy = col.GetComponentInParent<EnemyBase>();
            if (enemy == null) return;

            if (type != HitType.Block)
            {
                float multiplier = combat.GetDamageMultiplier(type);
                enemy.TakeDamage(combat.GetAttackDamage() * multiplier);
            }
            else
            {
                var rb = enemy.GetComponent<Rigidbody2D>();
                if (rb == null) return;

                KnockbackHandler.Apply(
                    rb,
                    transform.position, // 플레이어 위치 기준
                    6f,                  // 넉백 힘
                    1f
                );
            }
        }
    }
}