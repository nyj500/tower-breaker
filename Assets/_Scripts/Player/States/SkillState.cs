using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class SkillState : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        private float castTimer  = 0f;
        private float castLength = 0.5f; // TODO: SkillDataSO.duration에서 읽어오기

        public SkillState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm  = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            // TODO: SkillExecutor.ExecuteSkill() 호출 (슬롯 인덱스는 입력 UI에서 결정)
            ctrl.Anim.PlaySkill();
            castTimer = 0f;
        }

        public void Exit()
        {
            // TODO: 스킬 종료 처리
        }

        public void Update()
        {
            castTimer += Time.deltaTime;

            // TODO: 스킬 시전 완료 시 IdleState로 전환
            if (castTimer >= castLength)
                fsm.ChangeState(fsm.Idle);
        }

        public void FixedUpdate() { }
    }
}
