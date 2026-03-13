using System.Collections;
using UnityEngine;
using TowerBreaker.Enemy;

namespace TowerBreaker.Enemy.EnemyTypes
{
    /// <summary>
    /// 보스 2: 소환 + 원거리 연사 패턴.
    /// Phase 1 - 3발 연사
    /// Phase 2 - 소환 + 5발 연사
    /// </summary>
    public class Boss02 : BossController
    {
        [Header("Boss02 Config")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject minionPrefab;
        [SerializeField] private int phase1ShotCount = 3;
        [SerializeField] private int phase2ShotCount = 5;
        [SerializeField] private float shotInterval = 0.25f;
        [SerializeField] private Transform[] summonPoints;

        private bool isActing = false;

        protected override void InitFSM()
        {
            // TODO: FSM 상태 초기화
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
                yield return StartCoroutine(BurstShot(phase1ShotCount));
            else
            {
                yield return StartCoroutine(SummonMinions());
                yield return StartCoroutine(BurstShot(phase2ShotCount));
            }

            yield return new WaitForSeconds(2f);
            isActing = false;
        }

        private IEnumerator BurstShot(int count)
        {
            // TODO: count회 투사체 발사, 각 발사 사이 shotInterval 대기
            for (int i = 0; i < count; i++)
            {
                FireProjectile();
                yield return new WaitForSeconds(shotInterval);
            }
        }

        private void FireProjectile()
        {
            // TODO: 플레이어 방향으로 projectilePrefab 발사
        }

        private IEnumerator SummonMinions()
        {
            // TODO: summonPoints에 minionPrefab 스폰, 소환 연출 재생
            yield return new WaitForSeconds(0.5f);
            foreach (var point in summonPoints)
                Instantiate(minionPrefab, point.position, Quaternion.identity);
        }

        protected override void EnterNextPhase(int phase)
        {
            // TODO: 페이즈 전환 연출
        }

        protected override void OnDeath()
        {
            // TODO: 사망 연출, 보상 드롭
        }
    }
}
