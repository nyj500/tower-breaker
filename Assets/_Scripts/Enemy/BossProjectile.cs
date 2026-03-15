using UnityEngine;
using TowerBreaker.Player;

namespace TowerBreaker.Enemy
{
    public class BossProjectile : MonoBehaviour
    {
        [SerializeField] private float speed = 8f;
        [SerializeField] private float lifeTime = 2f;
        [SerializeField] private float knockbackForce = 10f;

        private Vector2 direction;
        private float timer;

        public void Init(Vector2 dir)
        {
            direction = dir.normalized;
            timer = 0f;
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
            var player = col.GetComponentInParent<PlayerController>();
            if (player == null) return;

            player.TakeKnockback(transform.position, knockbackForce);
            gameObject.SetActive(false);
        }
    }
}
