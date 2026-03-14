using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class IdleState : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        public IdleState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            ctrl.StopMove();
        }

        public void Exit() { }

        public void Update()
        {
            if (ctrl.AttackPressed)  { fsm.ChangeState(fsm.Attack); return; }
            if (ctrl.DashPressed)    { fsm.ChangeState(fsm.Dash); return; }
            if (ctrl.BlockPressed)   { fsm.ChangeState(fsm.Block); return; }
            if (ctrl.Skill1Pressed)  { fsm.ChangeState(fsm.Skill1); return; }
            if (ctrl.Skill2Pressed)  { fsm.ChangeState(fsm.Skill2); return; }
            if (ctrl.Skill3Pressed)  { fsm.ChangeState(fsm.Skill3); return; }
        }

        public void FixedUpdate() { }
    }
}
