using UnityEngine;
using TowerBreaker.Enemy;

namespace TowerBreaker.Enemy.EnemyTypes
{
    /// <summary>
    /// 일반 적 2: 원거리 투사체형.
    /// 일정 거리 유지하며 주기적으로 투사체 발사.
    /// </summary>
    public class RangedEnemy : EnemyBase
    {
        [Header("Ranged Config")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float preferredDistance = 5f;

        private float attackTimer = 0f;

        protected override void InitFSM()
        {
            // TODO: FSM 상태 초기화
        }

        private void Update()
        {
            if (isDead || playerTransform == null) return;

            float dist = Vector2.Distance(transform.position, playerTransform.position);

            if (dist < preferredDistance)
            {
                // TODO: 플레이어에서 멀어지는 방향으로 후퇴
                Vector2 retreatDir = (transform.position - playerTransform.position).normalized;
                rb.linearVelocity = retreatDir * data.moveSpeed;
            }
            else if (dist > data.detectionRange)
            {
                rb.linearVelocity = Vector2.zero;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
                TryShoot();
            }
        }

        private void TryShoot()
        {
            // TODO: 쿨다운 체크 후 projectilePrefab 스폰, 플레이어 방향으로 발사
            attackTimer += Time.deltaTime;
            if (attackTimer >= data.attackCooldown)
            {
                attackTimer = 0f;
                // TODO: ObjectPoolManager.Get("Projectile_Enemy", ...)
            }
        }

        protected override void OnDeath()
        {
            // TODO: 사망 VFX, ObjectPool 반환
        }
    }
}
