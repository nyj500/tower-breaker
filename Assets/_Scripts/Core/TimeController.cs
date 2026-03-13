using System.Collections;
using UnityEngine;

namespace TowerBreaker.Core
{
    public class TimeController : MonoBehaviour
    {
        public static TimeController Instance { get; private set; }

        private Coroutine hitStopCoroutine;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        /// <summary>
        /// 히트스톱: duration 동안 timeScale을 slowScale로 낮춘다.
        /// </summary>
        public void DoHitStop(float duration, float slowScale = 0.05f)
        {
            // TODO: 기존 히트스톱 코루틴이 실행 중이면 중단 후 재시작
            if (hitStopCoroutine != null)
                StopCoroutine(hitStopCoroutine);
            hitStopCoroutine = StartCoroutine(HitStopRoutine(duration, slowScale));
        }

        private IEnumerator HitStopRoutine(float duration, float slowScale)
        {
            // TODO: Time.timeScale = slowScale, Time.fixedDeltaTime 보정
            //       unscaledTime 기준으로 duration 대기 후 정상 timeScale 복구
            Time.timeScale = slowScale;
            Time.fixedDeltaTime = 0.02f * slowScale;

            yield return new WaitForSecondsRealtime(duration);

            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            hitStopCoroutine = null;
        }

        private void OnDestroy()
        {
            // TODO: 씬 전환 시 timeScale 복구 안전 처리
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}
