using System.Collections;
using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class BlockState : PlayerState
    {
        public BlockState(PlayerStateMachine fsm, PlayerController ctrl) : base(fsm, ctrl) { }

        public override void Enter()
        {
            ctrl.BlockPressed = false;
            ctrl.Anim.PlayBlock();
            fsm.RunCoroutine(BlockCoroutine());
        }

        public override void Exit()
        {
            ctrl.StopMove();
            ctrl.Anim.PlayStop();
        }

        IEnumerator BlockCoroutine()
        {
            ctrl.Anim.PlayBlock();

            yield return new WaitForSeconds(0.2f);

            fsm.ChangeState(fsm.Idle);
        }
    }
}
