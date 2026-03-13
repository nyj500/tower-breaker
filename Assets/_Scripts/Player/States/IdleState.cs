using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class IdleState : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        public IdleState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm  = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            // TODO: 이동 속도 0, Idle 애니메이션 재생
            ctrl.Move(Vector2.zero);
            ctrl.Anim.SetSpeed(0f);
        }

        public void Exit()
        {
            // TODO: 필요 시 정리 작업
        }

        public void Update()
        {
            // TODO: MoveInput이 있으면 MoveState로 전환
            if (ctrl.MoveInput.sqrMagnitude > 0.01f)
                fsm.ChangeState(fsm.Move);

            // TODO: AttackPressed → AttackState
            if (ctrl.AttackPressed)
                fsm.ChangeState(fsm.Attack);

            // TODO: DashPressed → DashState
            if (ctrl.DashPressed)
                fsm.ChangeState(fsm.Dash);

            // TODO: BlockPressed → BlockState
            if (ctrl.BlockPressed)
                fsm.ChangeState(fsm.Block);

            // TODO: SkillPressed → SkillState
            if (ctrl.SkillPressed)
                fsm.ChangeState(fsm.Skill);
        }

        public void FixedUpdate() { }
    }
}
