using UnityEngine;

namespace TowerBreaker.Player.States
{
    public class HitState : PlayerState
    {
        private float staggerTimer = 0f;

        public HitState(PlayerStateMachine fsm, PlayerController ctrl) : base(fsm, ctrl) { }

        public override void Enter()
        {
            staggerTimer = 0f;
            if (!ctrl.IsKnockedBack) ctrl.StopMove();
            ctrl.Anim.PlayHit();
        }

        public override void Update()
        {
            staggerTimer += Time.deltaTime;
            if (staggerTimer >= ctrl.stats.hitStaggerDuration)
                fsm.ChangeState(fsm.Idle);
        }
    }
}
