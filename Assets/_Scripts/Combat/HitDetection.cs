using UnityEngine;
using System.Collections.Generic;

namespace TowerBreaker.Combat
{
    public class HitDetection : MonoBehaviour
    {
        /// <summary>
        /// 원형 범위 내의 대상 Collider2D 목록을 반환한다.
        /// </summary>
        public static Collider2D[] OverlapCircle(Vector2 origin, float radius, LayerMask layer)
        {
            // TODO: 히트 수 제한이 필요하면 버퍼 기반 NonAlloc으로 교체
            return Physics2D.OverlapCircleAll(origin, radius, layer);
        }

        /// <summary>
        /// 박스 범위 내의 대상 Collider2D 목록을 반환한다.
        /// </summary>
        public static Collider2D[] OverlapBox(Vector2 origin, Vector2 size, float angle, LayerMask layer)
        {
            return Physics2D.OverlapBoxAll(origin, size, angle, layer);
        }

        /// <summary>
        /// 특정 방향 레이캐스트로 첫 번째 대상만 탐지한다.
        /// </summary>
        public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, LayerMask layer)
        {
            // TODO: 다중 피격 필요 시 RaycastAll로 교체
            return Physics2D.Raycast(origin, direction, distance, layer);
        }

        /// <summary>
        /// 이미 적중한 대상을 제외하는 필터링.
        /// </summary>
        public static List<Collider2D> FilterAlreadyHit(Collider2D[] candidates, HashSet<Collider2D> alreadyHit)
        {
            // TODO: 콤보 중 동일 적 중복 피격 방지에 활용
            var result = new List<Collider2D>();
            foreach (var col in candidates)
                if (!alreadyHit.Contains(col))
                    result.Add(col);
            return result;
        }
    }
}
