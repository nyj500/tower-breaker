using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class DashState : PlayerState
    {
        private float dashTimer;
        private const float DashDuration = 0.2f;
        private const float DashDistance = 3f;

        public DashState(PlayerStateMachine fsm, PlayerController ctrl) : base(fsm, ctrl) { }

        public override void Enter()
        {
            ctrl.DashPressed = false;
            dashTimer = 0f;
            ctrl.DashMove(DashDistance, DashDuration);
            ctrl.Anim.PlayDash();
        }

        public override void Exit()
        {
            ctrl.StopMove();
            ctrl.Anim.PlayStop();
        }

        public override void Update()
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= DashDuration)
                fsm.ChangeState(fsm.Idle);
        }
    }
}
