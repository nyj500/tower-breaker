using UnityEngine;
using TowerBreaker.Data;
using TowerBreaker.Core;
using TowerBreaker.Enemy;
using TowerBreaker.Player.States;
namespace TowerBreaker.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] public CharacterStatsSO stats;

        private Rigidbody2D rb;

        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerCombat Combat { get; private set; }
        public PlayerAnimation Anim { get; private set; }

        // 입력 캐시
        public bool AttackPressed { get; set; }
        public bool DashPressed { get; set; }
        public bool BlockPressed { get; set; }
        public bool Skill1Pressed { get; set; }
        public bool Skill2Pressed { get; set; }
        public bool Skill3Pressed { get; set; }

        // 런타임 스탯
        public int CurrentHp { get; private set; }
        public int MaxHp => stats.maxHp;
        public bool IsDead { get; private set; }
        public bool IsFacingRight { get; private set; } = true;
        public bool IsEnemyColliding { get; set; } = false;

        private void Awake()
        {
            StateMachine = GetComponent<PlayerStateMachine>();
            Combat = GetComponent<PlayerCombat>();
            Anim = GetComponent<PlayerAnimation>();
            rb = GetComponent<Rigidbody2D>();

            CurrentHp = stats.maxHp;
        }

        public void DashMove(float distance, float duration)
        {
            float speed = distance / duration;
            float dir = IsFacingRight ? 1f : -1f;
            rb.linearVelocity = new Vector2(dir * speed, 0f);
        }

        public void StopMove()
        {
            rb.linearVelocity = Vector2.zero;
        }

        public void SetFacingDirection(bool facingRight)
        {
            if (IsFacingRight == facingRight) return;
            IsFacingRight = facingRight;
            transform.localScale = new Vector3(facingRight ? 1f : -1f, 1f, 1f);
        }

        /// <summary>
        /// 카메라 벽 충돌 시 호출 - HP 1 감소
        /// </summary>
        public void TakeWallDamage()
        {
            if (IsDead) return;

            CurrentHp -= 1;
            EventBus.Publish(new PlayerHpChangedEvent { CurrentHp = CurrentHp, MaxHp = MaxHp });

            if (CurrentHp <= 0)
            {
                CurrentHp = 0;
                IsDead = true;
                StateMachine.ChangeState(StateMachine.Die);
            }
            else
            {
                StateMachine.ChangeState(StateMachine.Hit);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var enemy = collision.gameObject.GetComponent<EnemyBase>();
            if (enemy == null) return;

            IsEnemyColliding = true;
            // 대쉬 중 살아있는 적과 충돌 시 멈춤
            var state = StateMachine.CurrentState;
            if (state is DashState || state is Skill1State)
            {
                if (!enemy.IsDead)
                {
                    StopMove();
                    StateMachine.ChangeState(StateMachine.Idle);
                }
            }
        }

        public Rigidbody2D Rb => rb;
    }

    public struct PlayerHpChangedEvent
    {
        public int CurrentHp;
        public int MaxHp;
    }
}
