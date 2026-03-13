using UnityEngine;
using TowerBreaker.Equipment;

namespace TowerBreaker.Player.States
{
    public class BlockState : IPlayerState
    {
        private readonly PlayerStateMachine fsm;
        private readonly PlayerController ctrl;

        private float blockTimer     = 0f;
        private float cooldownTimer  = 0f;
        private bool  onCooldown     = false;

        public bool IsBlocking { get; private set; } = false;

        public BlockState(PlayerStateMachine fsm, PlayerController ctrl)
        {
            this.fsm  = fsm;
            this.ctrl = ctrl;
        }

        public void Enter()
        {
            // TODO: 방패 쿨다운 확인 → 쿨다운 중이면 IdleState로 즉시 전환
            if (onCooldown) { fsm.ChangeState(fsm.Idle); return; }

            IsBlocking = true;
            blockTimer = 0f;
            ctrl.Anim.PlayBlock(true);
            ctrl.Combat.ApplyBlock();
        }

        public void Exit()
        {
            // TODO: 블록 해제, 쿨다운 시작
            IsBlocking = false;
            onCooldown = true;
            cooldownTimer = 0f;
            ctrl.Anim.PlayBlock(false);
        }

        public void Update()
        {
            if (onCooldown)
            {
                // TODO: 쿨다운 경과 시 onCooldown = false
                float cd = 3f; // TODO: inventory에서 ShieldDataSO.blockCooldown 읽기
                cooldownTimer += Time.deltaTime;
                if (cooldownTimer >= cd) onCooldown = false;
                return;
            }

            // TODO: BlockPressed 해제 시 IdleState로 전환
            if (!ctrl.BlockPressed)
                fsm.ChangeState(fsm.Idle);
        }

        public void FixedUpdate() { }
    }
}
