using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class MoveState : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        public MoveState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm  = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            // TODO: 이동 애니메이션 재생
        }

        public void Exit()
        {
            // TODO: 이동 관련 상태 정리
        }

        public void Update()
        {
            // TODO: 이동 입력 없으면 IdleState로 전환
            if (ctrl.MoveInput.sqrMagnitude <= 0.01f)
                fsm.ChangeState(fsm.Idle);

            // TODO: 공격/대시/블록/스킬 입력 우선순위 처리
            if (ctrl.AttackPressed) { fsm.ChangeState(fsm.Attack); return; }
            if (ctrl.DashPressed)   { fsm.ChangeState(fsm.Dash);   return; }
            if (ctrl.BlockPressed)  { fsm.ChangeState(fsm.Block);  return; }
            if (ctrl.SkillPressed)  { fsm.ChangeState(fsm.Skill);  return; }

            // TODO: 이동 방향에 따라 캐릭터 스프라이트 플립
        }

        public void FixedUpdate()
        {
            // TODO: ctrl.Move(ctrl.MoveInput)로 물리 이동 적용
            ctrl.Move(ctrl.MoveInput.normalized);
            ctrl.Anim.SetSpeed(ctrl.MoveInput.magnitude);
        }
    }
}
