using UnityEngine;

namespace TowerBreaker.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator animator;

        // Animator 파라미터 해시 (성능 최적화)
        private static readonly int HashSpeed    = Animator.StringToHash("Speed");
        private static readonly int HashAttack   = Animator.StringToHash("Attack");
        private static readonly int HashCombo    = Animator.StringToHash("ComboStep");
        private static readonly int HashDash     = Animator.StringToHash("Dash");
        private static readonly int HashBlock    = Animator.StringToHash("Block");
        private static readonly int HashHit      = Animator.StringToHash("Hit");
        private static readonly int HashDie      = Animator.StringToHash("Die");
        private static readonly int HashSkill    = Animator.StringToHash("Skill");

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetSpeed(float speed)
        {
            // TODO: 이동 속도를 블렌드 트리 파라미터에 반영
            animator.SetFloat(HashSpeed, speed);
        }

        public void PlayAttack(int comboStep)
        {
            // TODO: 콤보 단계 파라미터 설정 후 Attack 트리거
            animator.SetInteger(HashCombo, comboStep);
            animator.SetTrigger(HashAttack);
        }

        public void PlayDash()   => animator.SetTrigger(HashDash);
        public void PlayBlock(bool isBlocking) => animator.SetBool(HashBlock, isBlocking);
        public void PlayHit()    => animator.SetTrigger(HashHit);
        public void PlayDie()    => animator.SetTrigger(HashDie);
        public void PlaySkill()  => animator.SetTrigger(HashSkill);
    }
}
