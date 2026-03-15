using UnityEngine;

namespace TowerBreaker.Player
{
    public abstract class PlayerState
    {
        protected readonly PlayerStateMachine fsm;
        protected readonly PlayerController ctrl;

        protected PlayerState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm = fsm;
            this.ctrl = ctrl;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}
