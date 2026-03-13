using UnityEngine;

namespace TowerBreaker.Combat
{
    public class KnockbackHandler : MonoBehaviour
    {
        /// <summary>
        /// 대상 Rigidbody2D에 넉백 힘을 적용한다.
        /// </summary>
        /// <param name="targetRb">넉백 받을 오브젝트의 Rigidbody2D</param>
        /// <param name="source">힘의 발원 위치</param>
        /// <param name="force">넉백 강도</param>
        /// <param name="resistance">넉백 저항 (0=완전저항, 1=그대로 적용)</param>
        public static void Apply(Rigidbody2D targetRb, Vector2 source, float force, float resistance = 1f)
        {
            // TODO: 저항값 적용하여 실제 힘 계산
            if (targetRb == null) return;

            float actualForce = force * resistance;
            if (actualForce <= 0f) return;

            Vector2 dir = ((Vector2)targetRb.transform.position - source).normalized;
            targetRb.AddForce(dir * actualForce, ForceMode2D.Impulse);
        }

        /// <summary>
        /// 일정 시간 후 속도를 0으로 리셋한다.
        /// </summary>
        public static System.Collections.IEnumerator ResetVelocity(Rigidbody2D rb, float delay)
        {
            // TODO: 넉백 후 자연스러운 감속이 필요하면 linearDamping 대신 사용
            yield return new WaitForSeconds(delay);
            if (rb != null)
                rb.linearVelocity = Vector2.zero;
        }
    }
}
