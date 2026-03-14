using System.Collections;
using UnityEngine;

namespace TowerBreaker.Player.States
{
    /// <summary>
    /// 스킬 1: 대쉬하며 공격
    /// </summary>
    public class Skill3State : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        public Skill3State(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            ctrl.Skill1Pressed = false;
            fsm.RunCoroutine(Skill3Coroutine());
        }

        public void Exit()
        {
            ctrl.StopMove();
        }

        public void Update() { }

        public void FixedUpdate() { }

        IEnumerator Skill3Coroutine()
        {
            ctrl.Anim.PlaySkill3();

            ctrl.Combat.EnableHitBox(this);

            yield return new WaitForSeconds(0.2f);

            ctrl.Combat.DisableHitBox(this);

            fsm.ChangeState(fsm.Idle);
        }
    }
}
