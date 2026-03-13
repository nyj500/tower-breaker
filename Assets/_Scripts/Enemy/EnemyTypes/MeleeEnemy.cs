using UnityEngine;
using TowerBreaker.Enemy;
using TowerBreaker.Core;
using TowerBreaker.Combat;

namespace TowerBreaker.Enemy.EnemyTypes
{
    /// <summary>
    /// 일반 적 1: 근접 공격형.
    /// 플레이어를 향해 직선 추격 후 근접 공격.
    /// </summary>
    public class MeleeEnemy : EnemyBase
    {
        private float attackTimer = 0f;

        protected override void InitFSM()
        {
            // TODO: Idle → Chase (플레이어 감지 시) → Attack (사거리 내) 전환 구현
        }

        private void Update()
        {
            if (isDead) return;

            if (IsInRange(data.attackRange))
            {
                AttackPlayer();
            }
            else if (IsInRange(data.detectionRange))
            {
                // TODO: 플레이어 방향으로 추격
                MoveToward(playerTransform.position, data.moveSpeed);
            }
            else
            {
                // TODO: Idle (제자리 대기 또는 순찰)
                rb.linearVelocity = Vector2.zero;
            }
        }

        private void AttackPlayer()
        {
            // TODO: 공격 쿨다운 체크 후 플레이어에게 data.attack 데미지 적용
            attackTimer += Time.deltaTime;
            if (attackTimer >= data.attackCooldown)
            {
                attackTimer = 0f;
                // TODO: 플레이어 TakeDamage 호출
            }
        }

        protected override void OnDeath()
        {
            // TODO: 사망 VFX 재생, ObjectPoolManager에 반환
        }
    }
}
