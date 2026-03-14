using UnityEngine;

namespace TowerBreaker.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator animator;

        private static readonly int HashAttack = Animator.StringToHash("Attack");
        private static readonly int HashDash = Animator.StringToHash("Dash");
        private static readonly int HashBlock = Animator.StringToHash("Block");
        private static readonly int HashHit = Animator.StringToHash("Hit");
        private static readonly int HashDead = Animator.StringToHash("Dead");
        private static readonly int HashSkill1 = Animator.StringToHash("Skill1");
        private static readonly int HashSkill2 = Animator.StringToHash("Skill2");
        private static readonly int HashSkill3 = Animator.StringToHash("Skill3");
        private static readonly int Stop = Animator.StringToHash("Stop");


        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public void PlayAttack() => animator?.SetTrigger(HashAttack);
        public void PlayDash() => animator?.SetTrigger(HashDash);
        public void PlayBlock() => animator?.SetTrigger(HashBlock);
        public void PlayHit() => animator?.SetTrigger(HashHit);
        public void PlayDead() => animator?.SetTrigger(HashDead);
        public void PlaySkill1() => animator?.SetTrigger(HashSkill1);
        public void PlaySkill2() => animator?.SetTrigger(HashSkill2);
        public void PlaySkill3() => animator?.SetTrigger(HashSkill3);
        public void PlayStop() => animator?.SetTrigger(Stop);
    }
}
