using UnityEngine;
using TowerBreaker.Enemy;

namespace TowerBreaker.Enemy.EnemyTypes
{
    /// <summary>
    /// 일반 적 3: 방패 보유형.
    /// 정면 공격을 블록하고 블록 성공 후 반격.
    /// </summary>
    public class ShieldEnemy : EnemyBase
    {
        [Header("Shield Config")]
        [SerializeField] private float blockCooldown = 4f;
        [SerializeField] private float counterDamageMultiplier = 1.5f;

        private bool  isBlocking    = true;
        private float blockTimer    = 0f;

        protected override void InitFSM()
        {
            // TODO: FSM 상태 초기화
        }

        private void Update()
        {
            if (isDead) return;

            if (!isBlocking)
            {
                blockTimer += Time.deltaTime;
                if (blockTimer >= blockCooldown)
                {
                    isBlocking = true;
                    blockTimer = 0f;
                    // TODO: 방패 들기 애니메이션 재생
                }
            }

            if (IsInRange(data.attackRange))
                TryCounter();
            else if (IsInRange(data.detectionRange))
                MoveToward(playerTransform.position, data.moveSpeed);
        }

        public override void TakeDamage(float damage)
        {
            // TODO: 블록 중이고 정면 공격이면 damage 무효, OnBlock() 호출
            if (isBlocking)
            {
                OnBlock();
                return;
            }
            base.TakeDamage(damage);
        }

        private void OnBlock()
        {
            // TODO: 블록 성공 애니메이션, 쿨다운 시작
            isBlocking = false;
            blockTimer = 0f;
            TryCounter();
        }

        private void TryCounter()
        {
            // TODO: 플레이어에게 counterDamageMultiplier * data.attack 데미지 반격
        }

        protected override void OnDeath()
        {
            // TODO: 방패 드롭 연출, ObjectPool 반환
        }
    }
}
