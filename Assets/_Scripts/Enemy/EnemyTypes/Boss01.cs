using UnityEngine;
using System.Collections;
using TowerBreaker.Core;
namespace TowerBreaker.Enemy.EnemyTypes
{
    public class Boss01 : EnemyBase
    {
        [Header("Knockback Attack")]
        [SerializeField] private float knockbackInterval = 3f;
        [SerializeField] private float attackAnimDuration = 1f;
        [SerializeField] private GameObject attackVFX;

        private static readonly int HashAttack = Animator.StringToHash("Attack");
        private static readonly int HashWalk = Animator.StringToHash("Walk");

        private float knockbackTimer;
        private bool isAttacking;

        protected override void InitFSM() { }

        protected override void Start()
        {
            base.Start();
            knockbackTimer = knockbackInterval;
        }

        private void Update()
        {
            if (isDead || !isActive || IsKnockedBack || isAttacking) return;

            rb.linearVelocity = Vector2.left * data.moveSpeed;

            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0f)
            {
                knockbackTimer = knockbackInterval;
                StartCoroutine(AttackRoutine());
            }
        }

        private IEnumerator AttackRoutine()
        {
            isAttacking = true;
            rb.linearVelocity = Vector2.zero;

            animator.SetTrigger(HashAttack);
            if (attackVFX != null) { attackVFX.SetActive(false); attackVFX.SetActive(true); }
            yield return new WaitForSeconds(attackAnimDuration);

            animator.SetBool(HashWalk, true);
            isAttacking = false;
        }

        protected override void OnDeath()
        {
            rb.linearVelocity = Vector2.zero;
            ObjectPoolManager.Instance.Return(PoolPrefab, gameObject);
        }
    }
}
