using System.Collections;
using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class AttackState : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        private bool hasDealtDamage;

        private Coroutine attackCoroutine;

        public AttackState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            ctrl.AttackPressed = false;
            hasDealtDamage = false;
            attackCoroutine = fsm.RunCoroutine(AttackCoroutine());
        }

        public void Exit() { }

        public void Update() { }

        public void FixedUpdate() { }

        IEnumerator AttackCoroutine()
        {
            ctrl.Anim.PlayAttack();

            ctrl.Combat.EnableHitBox(this);

            yield return new WaitForSeconds(0.2f);

            ctrl.Combat.DisableHitBox(this);

            fsm.ChangeState(fsm.Idle);
        }
    }
}
