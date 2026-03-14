using UnityEngine;
using TowerBreaker.Core;

namespace TowerBreaker.Player.States
{
    public class DieState : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        public DieState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm  = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            // TODO: 사망 애니메이션 재생, 입력 비활성화
            ctrl.StopMove();
            ctrl.Anim.PlayDead();

            // TODO: 일정 시간 후 GameManager.OnPlayerDead() 호출 (코루틴 or invoke)
            GameManager.Instance.OnPlayerDead();
        }

        public void Exit()
        {
            // TODO: 부활 로직이 있을 경우 처리 (현재는 미사용)
        }

        public void Update()
        {
            // 사망 상태에서는 업데이트 없음
        }

        public void FixedUpdate() { }
    }
}
