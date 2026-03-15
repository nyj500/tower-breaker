using UnityEngine;
using TowerBreaker.Player.States;
using System.Collections;

namespace TowerBreaker.Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private PlayerState currentState;

        public IdleState Idle { get; private set; }
        public AttackState Attack { get; private set; }
        public DashState Dash { get; private set; }
        public BlockState Block { get; private set; }
        public Skill1State Skill1 { get; private set; }
        public Skill2State Skill2 { get; private set; }
        public Skill3State Skill3 { get; private set; }
        public HitState Hit { get; private set; }
        public DieState Die { get; private set; }

        private void Awake()
        {
            var ctrl = GetComponent<PlayerController>();
            Idle = new IdleState(this, ctrl);
            Attack = new AttackState(this, ctrl);
            Dash = new DashState(this, ctrl);
            Block = new BlockState(this, ctrl);
            Skill1 = new Skill1State(this, ctrl);
            Skill2 = new Skill2State(this, ctrl);
            Skill3 = new Skill3State(this, ctrl);
            Hit = new HitState(this, ctrl);
            Die = new DieState(this, ctrl);
        }

        private void Start()
        {
            ChangeState(Idle);
        }

        private void Update()
        {
            currentState?.Update();
        }

        private void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }

        public void ChangeState(PlayerState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        public PlayerState CurrentState => currentState;

        public Coroutine RunCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }
}
