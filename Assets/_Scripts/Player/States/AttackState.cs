using System.Collections;
using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class AttackState : PlayerState
    {
        public AttackState(PlayerStateMachine fsm, PlayerController ctrl) : base(fsm, ctrl) { }

        public override void Enter()
        {
            ctrl.AttackPressed = false;
            fsm.RunCoroutine(AttackCoroutine());
        }

        IEnumerator AttackCoroutine()
        {
            ctrl.Anim.PlayAttack();

            yield return new WaitForSeconds(0.2f);

            fsm.ChangeState(fsm.Idle);
        }
    }
}
