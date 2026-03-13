using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class AttackState : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        private int   comboStep   = 0;
        private float comboTimer  = 0f;
        private bool  attackFired = false;

        // 콤보 윈도우: 다음 공격 입력을 받아줄 수 있는 최대 시간
        private const float ComboWindow = 0.6f;

        public AttackState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm  = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            // TODO: 콤보 초기화 및 첫 번째 공격 실행
            attackFired = false;
            comboTimer  = 0f;
        }

        public void Exit()
        {
            // TODO: 콤보 스텝 초기화
            comboStep = 0;
        }

        public void Update()
        {
            comboTimer += Time.deltaTime;

            if (!attackFired)
            {
                // TODO: ctrl.Combat.ExecuteAttack(comboStep) 호출, 애니메이션 재생
                ctrl.Combat.ExecuteAttack(comboStep);
                ctrl.Anim.PlayAttack(comboStep);
                attackFired = true;
            }

            // TODO: 콤보 윈도우 내 AttackPressed 입력 감지 → 다음 콤보 스텝 진행
            if (ctrl.AttackPressed && comboTimer < ComboWindow)
            {
                comboStep++;
                // TODO: 장착 무기의 comboSteps 최대치 확인
                int maxCombo = ctrl.stats != null ? 3 : 3;
                if (comboStep >= maxCombo) comboStep = 0;
                attackFired = false;
                comboTimer  = 0f;
            }

            // TODO: 콤보 윈도우 초과 → IdleState로 전환
            if (comboTimer >= ComboWindow && attackFired)
                fsm.ChangeState(fsm.Idle);
        }

        public void FixedUpdate() { }
    }
}
