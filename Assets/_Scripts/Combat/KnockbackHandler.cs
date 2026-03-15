using UnityEngine;
using System.Collections;

namespace TowerBreaker.Combat
{
    public static class KnockbackHandler
    {
        public static void Apply(MonoBehaviour runner, Rigidbody2D targetRb, Vector2 source, float force, float duration = 0.3f)
        {
            if (targetRb == null) return;
            if (force <= 0f) return;

            runner.StartCoroutine(KnockbackRoutine(targetRb, source, force, duration));
        }

        static IEnumerator KnockbackRoutine(Rigidbody2D rb, Vector2 source, float force, float duration)
        {
            var enemy = rb.GetComponent<Enemy.EnemyBase>();

            // 히트스탑 (잠깐 멈추는 연출)
            if (enemy != null) enemy.IsKnockedBack = true;
            rb.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(0.05f);

            // 넉백 방향 계산 (source → target)
            Vector2 dir = ((Vector2)rb.transform.position - source).normalized;
            rb.linearVelocity = dir * force;

            // 감속
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                rb.linearVelocity = Vector2.Lerp(dir * force, Vector2.zero, t * t);
                yield return null;
            }

            rb.linearVelocity = Vector2.zero;
            if (enemy != null) enemy.IsKnockedBack = false;
        }
    }
}
