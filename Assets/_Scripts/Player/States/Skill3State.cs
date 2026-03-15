using System.Collections;
using UnityEngine;
using TowerBreaker.Combat;

namespace TowerBreaker.Player.States
{
    public class Skill3State : PlayerState
    {
        public Skill3State(PlayerStateMachine fsm, PlayerController ctrl) : base(fsm, ctrl) { }

        public override void Enter()
        {
            ctrl.Skill3Pressed = false;
            fsm.RunCoroutine(Skill3Coroutine());
        }

        IEnumerator Skill3Coroutine()
        {
            ctrl.Anim.PlaySkill3();

            yield return new WaitForSeconds(0.2f);

            ctrl.Combat.FireProjectile();
            fsm.ChangeState(fsm.Idle);
        }
    }
}
