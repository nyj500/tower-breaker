using System.Collections;
using UnityEngine;

namespace TowerBreaker.Player.States
{
    /// <summary>
    /// 스킬 1: 대쉬하며 공격
    /// </summary>
    public class Skill2State : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        public Skill2State(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            ctrl.Skill1Pressed = false;
            fsm.RunCoroutine(Skill2Coroutine());
        }

        public void Exit()
        {
            ctrl.StopMove();
        }

        public void Update() { }

        public void FixedUpdate() { }

        IEnumerator Skill2Coroutine()
        {
            ctrl.Anim.PlaySkill2();

            ctrl.Combat.EnableHitBox(this);

            yield return new WaitForSeconds(0.5f);

            ctrl.Combat.DisableHitBox(this);

            fsm.ChangeState(fsm.Idle);
        }
    }
}
