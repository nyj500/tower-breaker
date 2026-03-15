using System.Collections;
using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class Skill2State : PlayerState
    {
        public Skill2State(PlayerStateMachine fsm, PlayerController ctrl) : base(fsm, ctrl) { }

        public override void Enter()
        {
            ctrl.Skill2Pressed = false;
            fsm.RunCoroutine(Skill2Coroutine());
        }

        public override void Exit()
        {
            ctrl.StopMove();
        }

        IEnumerator Skill2Coroutine()
        {
            ctrl.Anim.PlaySkill2();

            yield return new WaitForSeconds(0.5f);

            fsm.ChangeState(fsm.Idle);
        }
    }
}
