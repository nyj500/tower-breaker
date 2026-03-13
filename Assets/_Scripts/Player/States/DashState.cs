using System.Collections;
using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class DashState : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        public bool isInvincible { get; private set; } = false;

        private float dashTimer    = 0f;
        private bool  dashStarted  = false;
        private Vector2 dashDir;

        public DashState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm  = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            // TODO: 대시 방향 결정 (입력 방향 또는 캐릭터 바라보는 방향)
            dashDir     = ctrl.MoveInput.sqrMagnitude > 0.01f
                          ? ctrl.MoveInput.normalized
                          : Vector2.right * (ctrl.transform.localScale.x > 0 ? 1f : -1f);
            dashTimer   = 0f;
            dashStarted = false;
            isInvincible = true;

            ctrl.Anim.PlayDash();
        }

        public void Exit()
        {
            // TODO: 무적 해제, 속도 초기화
            isInvincible = false;
            ctrl.Move(Vector2.zero);
        }

        public void Update()
        {
            dashTimer += Time.deltaTime;

            // TODO: dashDuration 종료 시 IdleState로 전환
            if (dashTimer >= ctrl.stats.dashDuration)
                fsm.ChangeState(fsm.Idle);
        }

        public void FixedUpdate()
        {
            // TODO: 대시 거리 / dashDuration으로 속도 계산하여 rb에 적용
            float speed = ctrl.stats.dashDistance / ctrl.stats.dashDuration;
            ctrl.Move(dashDir * speed);
        }
    }
}
