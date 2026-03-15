using UnityEngine;
using System.Collections;
using TowerBreaker.Core;

namespace TowerBreaker.Enemy.EnemyTypes
{
    public class Boss02 : EnemyBase
    {
        [Header("Projectile")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireInterval = 3f;
        [SerializeField] private float fireAnimDuration = 0.5f;

        private static readonly int HashAttack = Animator.StringToHash("Attack");
        private static readonly int HashWalk = Animator.StringToHash("Walk");

        private float fireTimer;
        private bool isFiring;

        protected override void InitFSM() { }

        protected override void Start()
        {
            base.Start();
            fireTimer = fireInterval;
        }

        private void Update()
        {
            if (isDead || !isActive || isFiring) return;

            rb.linearVelocity = Vector2.left * data.moveSpeed;

            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                fireTimer = fireInterval;
                StartCoroutine(FireRoutine());
            }
        }

        private IEnumerator FireRoutine()
        {
            isFiring = true;
            rb.linearVelocity = Vector2.zero;

            animator.SetTrigger(HashAttack);
            yield return new WaitForSeconds(fireAnimDuration);

            FireProjectile();

            animator.SetBool(HashWalk, true);
            isFiring = false;
        }

        private void FireProjectile()
        {
            if (projectilePrefab == null) return;

            Transform spawnPoint = firePoint != null ? firePoint : transform;
            var obj = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            var proj = obj.GetComponent<BossProjectile>();
            proj?.Init(Vector2.left);
        }

        protected override void OnDeath()
        {
            rb.linearVelocity = Vector2.zero;
            ObjectPoolManager.Instance.Return(PoolPrefab, gameObject);
        }
    }
}
