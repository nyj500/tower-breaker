using System.Collections;
using UnityEngine;

namespace TowerBreaker.Core
{
    public class CameraShaker : MonoBehaviour
    {
        public static CameraShaker Instance { get; private set; }

        [SerializeField] private Camera targetCamera;

        private Vector3 originalPosition;
        private Coroutine shakeCoroutine;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;

            if (targetCamera == null)
                targetCamera = Camera.main;
        }

        /// <summary>
        /// 카메라를 duration 동안 magnitude 강도로 흔든다.
        /// </summary>
        public void Shake(float duration, float magnitude)
        {
            // TODO: 기존 코루틴이 실행 중이면 중단하고 새로 시작
            if (shakeCoroutine != null)
                StopCoroutine(shakeCoroutine);
            shakeCoroutine = StartCoroutine(ShakeRoutine(duration, magnitude));
        }

        private IEnumerator ShakeRoutine(float duration, float magnitude)
        {
            // TODO: duration 동안 Random.insideUnitSphere * magnitude 로 카메라 위치 오프셋 적용
            //       종료 시 originalPosition으로 복구
            originalPosition = targetCamera.transform.localPosition;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                Vector3 offset = Random.insideUnitSphere * magnitude;
                offset.z = 0f;
                targetCamera.transform.localPosition = originalPosition + offset;

                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }

            targetCamera.transform.localPosition = originalPosition;
            shakeCoroutine = null;
        }
    }
}
