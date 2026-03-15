using UnityEngine;
using System.Collections.Generic;
using TowerBreaker.Enemy;
using TowerBreaker.Player;

namespace TowerBreaker.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 1.2f;

        private float damage;
        private Vector2 direction;
        private float timer;
        private HitFeedback feedback;
        private HashSet<EnemyBase> hitEnemies = new();

        public void Init(float damage, bool facingRight, HitFeedback hitFeedback = null)
        {
            this.damage = damage;
            feedback = hitFeedback;
            direction = facingRight ? Vector2.right : Vector2.left;
            timer = 0f;
            hitEnemies.Clear();

            // 방향에 맞게 스프라이트 뒤집기 (프리팹 스케일 유지)
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (facingRight ? 1f : -1f);
            transform.localScale = scale;
        }

        private void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            timer += Time.deltaTime;
            if (timer >= lifeTime)
                gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var enemy = col.GetComponentInParent<EnemyBase>();
            if (enemy == null || enemy.IsDead) return;
            if (!hitEnemies.Add(enemy)) return;

            enemy.TakeDamage(damage);
            feedback?.PlayHeavyHit(transform.position);
        }
    }
}
