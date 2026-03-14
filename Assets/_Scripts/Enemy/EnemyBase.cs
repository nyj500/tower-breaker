using UnityEngine;
using TowerBreaker.Data;
using TowerBreaker.Stage;

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
        protected bool isDead;
        protected bool isActive;
        protected bool getDamaged;

        public float CurrentHp => currentHp;
        public bool IsDead => isDead;
        public bool IsActive => isActive;
        public bool GetDamaged
        {
            get => getDamaged;
            set => getDamaged = value;
        }

        public GameObject PoolPrefab { get; set; } // ObjectPoolManager 반환용

        public void SetActive(bool active)
        {
            isActive = active;
            animator?.SetBool(HashWalk, active);
        }

        protected Rigidbody2D rb;
        protected Animator animator;
        protected EnemyStateMachine fsm;
        protected Transform playerTransform;

        private static readonly int HashWalk = Animator.StringToHash("Walk");

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            fsm = GetComponent<EnemyStateMachine>();
        }

        protected virtual void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            InitFSM();
        }

        public virtual void Reset()
        {
            currentHp = data.maxHp;
            isDead    = false;
            isActive  = false;
            FloorManager.Instance?.RegisterEnemy();
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
            FloorManager.Instance?.OnEnemyDied();
        }

        /// <summary>
        /// 사망 시 서브클래스별 고유 처리 (드롭, 연출 등).
        /// </summary>
        protected abstract void OnDeath();

        protected void MoveToward(Vector2 target, float speed)
        {
            // TODO: 직선 이동
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
