using System.Collections;
using UnityEngine;
using TowerBreaker.Core;

namespace TowerBreaker.FeedbackFX
{
    /// <summary>
    /// 파티클 VFX 풀링 관리자.
    /// ParticleSystem 종료 시 자동으로 Pool에 반환.
    /// </summary>
    public class VFXPooler : MonoBehaviour
    {
        [SerializeField] private GameObject prefab; // 자기 자신의 프리팹 참조
        [SerializeField] private ParticleSystem particle;

        private void Awake()
        {
            if (particle == null)
                particle = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            // TODO: 파티클 재생 시작, 완료 후 자동 반환 코루틴 시작
            particle?.Play();
            StartCoroutine(ReturnWhenDone());
        }

        private IEnumerator ReturnWhenDone()
        {
            // TODO: particle.isPlaying이 끝날 때까지 대기 후 풀 반환
            if (particle == null) yield break;

            yield return new WaitUntil(() => !particle.IsAlive(true));

            ObjectPoolManager.Instance?.Return(prefab, gameObject);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            particle?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        /// <summary>
        /// 외부에서 풀 키를 설정할 수 있도록 한다.
        /// </summary>
        public void SetPrefab(GameObject p) => prefab = p;
    }
}
