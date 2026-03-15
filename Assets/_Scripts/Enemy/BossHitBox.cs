using UnityEngine;
using TowerBreaker.Player;

namespace TowerBreaker.Enemy
{
    public class BossHitBox : MonoBehaviour
    {
        [SerializeField] private float knockbackForce = 12f;

        private void OnTriggerEnter2D(Collider2D col)
        {
            var player = col.GetComponentInParent<PlayerController>();
            if (player == null) return;

            player.TakeKnockback(transform.position, knockbackForce);
        }
    }
}
