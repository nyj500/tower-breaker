using UnityEngine;
using TowerBreaker.Core;

namespace TowerBreaker.Player.States
{
    public class DieState : PlayerState
    {
        public DieState(PlayerStateMachine fsm, PlayerController ctrl) : base(fsm, ctrl) { }

        public override void Enter()
        {
            ctrl.StopMove();
            ctrl.Anim.PlayDead();
            GameManager.Instance.OnPlayerDead();
        }
    }
}
