using System.Collections;
using UnityEngine;
using TowerBreaker.Core;
using TowerBreaker.Data;

namespace TowerBreaker.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPoints;

        /// <summary>
        /// 웨이브 데이터에 따라 적을 순차 스폰한다.
        /// </summary>
        public IEnumerator SpawnWave(WaveData wave)
        {
            // TODO: wave.enemyTypes 순회, ObjectPoolManager.Get()으로 적 꺼내기
            //       spawnPoints 중 랜덤 위치에 배치
            yield return new WaitForSeconds(wave.delayBeforeWave);

            for (int i = 0; i < wave.enemyTypes.Length; i++)
            {
                int count = wave.enemyCountPerType[i];
                EnemyDataSO enemyData = wave.enemyTypes[i];

                for (int j = 0; j < count; j++)
                {
                    // TODO: ObjectPoolManager에서 enemyData.enemyName 키로 꺼내기
                    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                    // TODO: 꺼낸 GameObject의 EnemyBase.data = enemyData 설정
                    yield return new WaitForSeconds(0.3f);
                }
            }
        }

        public void SpawnBoss(GameObject bossPrefab)
        {
            // TODO: 보스 스폰 포인트에 bossPrefab 인스턴스화 또는 풀에서 꺼내기
            Transform spawnPoint = spawnPoints[0];
            Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
