using UnityEngine;
using TowerBreaker.Core;

namespace TowerBreaker.Enemy.EnemyTypes
{
    /// <summary>
    /// 일반 적: 활성화 시 왼쪽으로 직선 이동.
    /// </summary>
    public class MeleeEnemy : EnemyBase
    {
        protected override void InitFSM() { }

        private void Update()
        {
            if (isDead || !isActive || IsKnockedBack) return;

            rb.linearVelocity = Vector2.left * data.moveSpeed;
        }

        protected override void OnDeath()
        {
            rb.linearVelocity = Vector2.zero;
            
            ObjectPoolManager.Instance.Return(PoolPrefab, gameObject);
        }
    }
}
