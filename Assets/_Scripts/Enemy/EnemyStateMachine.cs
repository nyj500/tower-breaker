using UnityEngine;

namespace TowerBreaker.Enemy
{
    public enum EnemyStateType { Idle, Chase, Attack, Hit, Die }

    public class EnemyStateMachine : MonoBehaviour
    {
        private IEnemyState currentState;
        public EnemyStateType CurrentStateType { get; private set; }

        public void ChangeState(IEnemyState newState, EnemyStateType type)
        {
            // TODO: 현재 상태 Exit → 새 상태 Enter
            currentState?.Exit();
            currentState = newState;
            CurrentStateType = type;
            currentState?.Enter();
        }

        private void Update()
        {
            currentState?.Update();
        }

        private void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }
    }
}
