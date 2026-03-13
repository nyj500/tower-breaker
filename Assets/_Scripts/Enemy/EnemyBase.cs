using UnityEngine;
using TowerBreaker.Core;
using TowerBreaker.Data;

namespace TowerBreaker.Enemy
{
    /// <summary>
    /// 모든 적 타입의 공통 베이스 클래스.
    /// 구체적 패턴/행동은 서브클래스에서 구현.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyStateMachine))]
    public abstract class EnemyBase : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] public EnemyDataSO data;

        // 런타임 스탯
        protected float currentHp;
        protected bool  isDead;

        protected Rigidbody2D     rb;
        protected EnemyStateMachine fsm;
        protected Transform       playerTransform;

        protected virtual void Awake()
        {
            rb  = GetComponent<Rigidbody2D>();
            fsm = GetComponent<EnemyStateMachine>();
        }

        protected virtual void Start()
        {
            // TODO: 플레이어 오브젝트 찾기 (태그 또는 GameManager 참조)
            currentHp = data.maxHp;
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

            StageManager.Instance?.RegisterEnemy();
            InitFSM();
        }

        /// <summary>
        /// 서브클래스에서 FSM 상태를 초기화한다.
        /// </summary>
        protected abstract void InitFSM();

        public virtual void TakeDamage(float damage)
        {
            // TODO: 데미지 적용, 피격 상태 전환
            if (isDead) return;
            currentHp -= damage;
            if (currentHp <= 0f)
            {
                currentHp = 0f;
                isDead    = true;
                Die();
            }
            else
            {
                OnHit();
            }
        }

        protected virtual void OnHit()
        {
            // TODO: Hit 상태 전환, 피격 VFX/SFX 재생
        }

        protected void Die()
        {
            // TODO: Die 상태 전환, 드롭 아이템 스폰, ObjectPool 반환
            OnDeath();
            StageManager.Instance?.OnEnemyDied();
        }

        /// <summary>
        /// 사망 시 서브클래스별 고유 처리 (드롭, 연출 등).
        /// </summary>
        protected abstract void OnDeath();

        protected void MoveToward(Vector2 target, float speed)
        {
            // TODO: 장애물 회피 없이 직선 이동 (NavMesh 연동 확장 가능)
            Vector2 dir = ((Vector2)target - (Vector2)transform.position).normalized;
            rb.linearVelocity = dir * speed;
        }

        protected bool IsInRange(float range)
        {
            if (playerTransform == null) return false;
            return Vector2.Distance(transform.position, playerTransform.position) <= range;
        }
    }
}
