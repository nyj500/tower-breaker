using UnityEngine;
using TowerBreaker.Data;

namespace TowerBreaker.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] public CharacterStatsSO stats;

        [Header("References")]
        [SerializeField] private Rigidbody2D rb;

        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerCombat Combat { get; private set; }
        public PlayerAnimation Anim { get; private set; }

        // 입력 캐시 (TouchInputUI에서 갱신)
        public Vector2 MoveInput { get; set; }
        public bool AttackPressed { get; set; }
        public bool DashPressed { get; set; }
        public bool BlockPressed { get; set; }
        public bool SkillPressed { get; set; }

        // 런타임 스탯
        public float CurrentHp { get; private set; }
        public bool IsDead { get; private set; }

        private void Awake()
        {
            StateMachine = GetComponent<PlayerStateMachine>();
            Combat = GetComponent<PlayerCombat>();
            Anim   = GetComponent<PlayerAnimation>();
            rb     = rb != null ? rb : GetComponent<Rigidbody2D>();

            CurrentHp = stats.maxHp;
        }

        private void Update()
        {
            // TODO: 터치 입력을 각 bool/Vector2 필드에 매핑 (TouchInputUI에서 직접 주입받도록 설계)
        }

        public void Move(Vector2 direction)
        {
            // TODO: rb.velocity를 direction * moveSpeed로 설정
            rb.linearVelocity = direction * stats.moveSpeed;
        }

        public void TakeDamage(float damage)
        {
            // TODO: 방어력 적용 후 HP 감소, 사망 판정
            if (IsDead) return;

            CurrentHp -= damage;
            if (CurrentHp <= 0f)
            {
                CurrentHp = 0f;
                IsDead = true;
                StateMachine.ChangeState(StateMachine.Die);
            }
            else
            {
                StateMachine.ChangeState(StateMachine.Hit);
            }

            // TODO: EventBus를 통해 HpChangedEvent 발행
        }

        public Rigidbody2D Rb => rb;
    }
}
