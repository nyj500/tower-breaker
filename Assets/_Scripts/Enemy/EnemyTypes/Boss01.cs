using System.Collections;
using UnityEngine;
using TowerBreaker.Enemy;
using TowerBreaker.Core;

namespace TowerBreaker.Enemy.EnemyTypes
{
    /// <summary>
    /// 보스 1: 돌진 + 범위 공격 패턴.
    /// Phase 1 - 돌진 반복
    /// Phase 2 - 돌진 + 범위 충격파
    /// </summary>
    public class Boss01 : BossController
    {
        [Header("Boss01 Config")]
        [SerializeField] private float chargeSpeed = 10f;
        [SerializeField] private float chargeWindupTime = 0.8f;
        [SerializeField] private float aoeRadius = 3f;
        [SerializeField] private LayerMask playerLayer;

        private bool isActing = false;

        protected override void InitFSM()
        {
            // TODO: Idle → Chase → 패턴 선택 루프
        }

        private void Update()
        {
            if (isDead || isActing) return;

            if (IsInRange(data.detectionRange))
                StartCoroutine(PhasePattern());
        }

        private IEnumerator PhasePattern()
        {
            isActing = true;

            if (currentPhase == 1)
                yield return StartCoroutine(ChargeAttack());
            else
                yield return StartCoroutine(ChargeWithAoE());

            yield return new WaitForSeconds(1.5f);
            isActing = false;
        }

        private IEnumerator ChargeAttack()
        {
            // TODO: 플레이어 방향 조준 → windup 대기 → 고속 돌진
            yield return new WaitForSeconds(chargeWindupTime);
            // TODO: 돌진 이동 처리
        }

        private IEnumerator ChargeWithAoE()
        {
            // TODO: ChargeAttack 후 착지 지점에서 AoE 충격파 발동
            yield return StartCoroutine(ChargeAttack());
            DoAoE();
        }

        private void DoAoE()
        {
            // TODO: aoeRadius 내 플레이어에게 data.attack * 1.5f 데미지
            //       CameraShaker.Instance.Shake() 호출
            CameraShaker.Instance?.Shake(0.4f, 0.3f);
        }

        protected override void EnterNextPhase(int phase)
        {
            // TODO: 페이즈 전환 연출 (플래시, 대사 등)
        }

        protected override void OnDeath()
        {
            // TODO: 보스 사망 연출, 보상 드롭
        }
    }
}
