using System.Collections;
using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class BlockState : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        private const float BlockDuration = 0.5f;

        public BlockState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            ctrl.BlockPressed = false;
            ctrl.Anim.PlayBlock();
            fsm.RunCoroutine(BlockCoroutine());
        }

        public void Exit()
        {            
            ctrl.StopMove();
            ctrl.Anim.PlayStop();
        }

        public void Update() { }

        public void FixedUpdate() { }

        IEnumerator BlockCoroutine()
        {
            ctrl.Anim.PlayBlock();

            ctrl.Combat.EnableHitBox(this);

            yield return new WaitForSeconds(0.2f);

            ctrl.Combat.DisableHitBox(this);

            fsm.ChangeState(fsm.Idle);
        }
    }
}
