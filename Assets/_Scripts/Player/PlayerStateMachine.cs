using UnityEngine;
using TowerBreaker.Player.States;

namespace TowerBreaker.Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private IPlayerState currentState;

        // 상태 인스턴스 캐싱
        public IdleState    Idle    { get; private set; }
        public MoveState    Move    { get; private set; }
        public AttackState  Attack  { get; private set; }
        public DashState    Dash    { get; private set; }
        public BlockState   Block   { get; private set; }
        public SkillState   Skill   { get; private set; }
        public HitState     Hit     { get; private set; }
        public DieState     Die     { get; private set; }

        private void Awake()
        {
            // TODO: 각 상태에 필요한 컴포넌트 참조를 주입하여 초기화
            var ctrl = GetComponent<PlayerController>();
            Idle   = new IdleState(this, ctrl);
            Move   = new MoveState(this, ctrl);
            Attack = new AttackState(this, ctrl);
            Dash   = new DashState(this, ctrl);
            Block  = new BlockState(this, ctrl);
            Skill  = new SkillState(this, ctrl);
            Hit    = new HitState(this, ctrl);
            Die    = new DieState(this, ctrl);
        }

        private void Start()
        {
            // TODO: 초기 상태를 Idle로 설정
            ChangeState(Idle);
        }

        private void Update()
        {
            currentState?.Update();
        }

        private void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }

        public void ChangeState(IPlayerState newState)
        {
            // TODO: 현재 상태 Exit → 새 상태 Enter
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        public IPlayerState CurrentState => currentState;
    }
}
