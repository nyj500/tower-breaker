using System.Collections;
using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class Skill1State : PlayerState
    {
        private float Duration = 0.3f;
        private float DashDistance = 4f;

        public Skill1State(PlayerStateMachine fsm, PlayerController ctrl) : base(fsm, ctrl) { }

        public override void Enter()
        {
            ctrl.Skill1Pressed = false;
            fsm.RunCoroutine(Skill1Coroutine());
        }

        public override void Exit()
        {
            ctrl.StopMove();
        }

        IEnumerator Skill1Coroutine()
        {
            ctrl.Anim.PlaySkill1();
            ctrl.DashMove(DashDistance, Duration);

            yield return new WaitForSeconds(Duration);

            ctrl.Anim.PlayStop();
            fsm.ChangeState(fsm.Idle);
        }
    }
}
