using UnityEngine;
using TowerBreaker.Stage;

namespace TowerBreaker.Player
{
    public class CameraBoundaryDetector : MonoBehaviour
    {
        [SerializeField] private float wallPushForce = 5f;

        private PlayerController player;
        private bool isDamageCooldown;

        private void Awake()
        {
            player = GetComponent<PlayerController>();
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            if (isDamageCooldown || player.IsDead) return;
            if (col.gameObject.GetComponent<CameraWall>() == null) return;

            player.TakeWallDamage();
            if (!player.IsDead)
                player.TakeKnockback(col.transform.position, wallPushForce);
            StartCoroutine(DamageCooldown());
        }

        private System.Collections.IEnumerator DamageCooldown()
        {
            isDamageCooldown = true;
            yield return new WaitForSeconds(2f);
            isDamageCooldown = false;
        }
    }
}
