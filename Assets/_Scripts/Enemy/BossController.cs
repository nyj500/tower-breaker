using UnityEngine;

namespace TowerBreaker.Enemy
{
    public abstract class BossController : EnemyBase
    {
        [Header("Boss Phase")]
        [SerializeField] protected int totalPhases = 2;
        protected int currentPhase = 1;

        protected float phaseChangeThreshold => data.maxHp / totalPhases;

        protected override void Start()
        {
            base.Start();
            // TODO: 보스 전용 UI(체력바 등) 활성화
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            // TODO: 체력 임계값 기반으로 페이즈 전환 확인
            CheckPhaseTransition();
        }

        private void CheckPhaseTransition()
        {
            // TODO: currentHp가 다음 페이즈 임계값 이하면 EnterNextPhase() 호출
            int expectedPhase = Mathf.CeilToInt(currentHp / phaseChangeThreshold);
            if (expectedPhase < currentPhase)
            {
                currentPhase = expectedPhase;
                EnterNextPhase(currentPhase);
            }
        }

        /// <summary>
        /// 서브클래스에서 페이즈별 패턴 전환 구현.
        /// </summary>
        protected abstract void EnterNextPhase(int phase);

        protected override void OnHit()
        {
            // TODO: 보스 피격 연출 (카메라 흔들림 등)
            base.OnHit();
        }
    }
}
