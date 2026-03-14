using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class DashState : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        private float dashTimer;
        private const float DashDuration = 0.2f;
        private const float DashDistance = 3f;

        public DashState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            ctrl.DashPressed = false;
            dashTimer = 0f;
            ctrl.DashMove(DashDistance, DashDuration);
            ctrl.Anim.PlayDash();
        }

        public void Exit()
        {
            ctrl.StopMove();
            ctrl.Anim.PlayStop();
        }

        public void Update()
        {
            dashTimer += Time.deltaTime;

            if (dashTimer >= DashDuration)
                fsm.ChangeState(fsm.Idle);
        }

        public void FixedUpdate() { }
    }
}
