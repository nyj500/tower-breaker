using UnityEngine;
using TowerBreaker.Enemy;
using TowerBreaker.Combat;
using TowerBreaker.Stage;

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
        private HitFeedback feedback;

        private void Awake()
        {
            combat = GetComponentInParent<PlayerCombat>();
            feedback = GetComponentInParent<HitFeedback>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var enemy = col.GetComponentInParent<EnemyBase>();
            if (enemy == null) return;

            if (type != HitType.Block)
            {
                float multiplier = combat.GetDamageMultiplier(type);
                enemy.TakeDamage(combat.GetAttackDamage() * multiplier);

                bool isHeavy = type == HitType.Skill1 || type == HitType.Skill2 || type == HitType.Skill3;
                if (isHeavy)
                    feedback?.PlayHeavyHit(col.transform.position);
                else
                    feedback?.PlayLightHit(col.transform.position);
            }
            else
            {
                feedback?.PlayLightHit(col.transform.position);
                KnockbackAllEnemies();
            }
        }

        private void KnockbackAllEnemies()
        {
            var enemies = FloorManager.Instance.GetCurrentFloorEnemies();
            foreach (var obj in enemies)
            {
                var enemy = obj.GetComponent<EnemyBase>();
                if (enemy == null || enemy.IsDead) continue;

                var rb = enemy.GetComponent<Rigidbody2D>();
                if (rb == null) continue;

                KnockbackHandler.Apply(this, rb, transform.position, 5f, 0.3f);
            }
        }
    }
}