using System.Collections;
using UnityEngine;

namespace TowerBreaker.Player.States
{
    /// <summary>
    /// 스킬 1: 대쉬하며 공격
    /// </summary>
    public class Skill1State : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        private float Duration = 0.3f;
        private float DashDistance = 4f;

        public Skill1State(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            ctrl.Skill1Pressed = false;
            fsm.RunCoroutine(Skill1Coroutine());
        }

        public void Exit()
        {
            ctrl.StopMove();
        }

        public void Update() { }

        public void FixedUpdate() { }

        IEnumerator Skill1Coroutine()
        {
            ctrl.Anim.PlaySkill1();
            ctrl.DashMove(DashDistance, Duration);

            ctrl.Combat.EnableHitBox(this);

            yield return new WaitForSeconds(0.2f);

            ctrl.Combat.DisableHitBox(this);
            ctrl.Anim.PlayStop();

            fsm.ChangeState(fsm.Idle);
        }
    }
}
