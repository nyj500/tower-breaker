using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class HitState : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        private float staggerTimer = 0f;

        public HitState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm  = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            // TODO: 경직 시작 - 이동 정지, Hit 애니메이션 재생
            staggerTimer = 0f;
            ctrl.StopMove();
            ctrl.Anim.PlayHit();
        }

        public void Exit()
        {
            // TODO: 경직 종료 처리
        }

        public void Update()
        {
            staggerTimer += Time.deltaTime;

            // TODO: staggerDuration(CharacterStatsSO) 경과 시 IdleState로 전환
            if (staggerTimer >= ctrl.stats.hitStaggerDuration)
                fsm.ChangeState(fsm.Idle);
        }

        public void FixedUpdate() { }
    }
}
