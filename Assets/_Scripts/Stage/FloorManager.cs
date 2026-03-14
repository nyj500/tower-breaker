using UnityEngine;
using System.Collections.Generic;
using TowerBreaker.Core;
using TowerBreaker.Data;
using TowerBreaker.Enemy;

namespace TowerBreaker.Stage
{
    public class FloorManager : MonoBehaviour
    {
        public static FloorManager Instance { get; private set; }

        [Header("Floors")]
        [SerializeField] private FloorDataSO[] floors;

        [Header("Spawn")]
        [SerializeField] private Transform[] floorSpawnPoints; // 층별 스폰 기준점 (index = 층)
        [SerializeField] private float spawnSpacing = 1f;

        private int floorIndex = 0;
        private int enemiesAlive = 0;

        public int CurrentFloor => floorIndex;

        private List<GameObject> currentFloorEnemies = new();
        private List<GameObject> nextFloorEnemies    = new();

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        private void Start()
        {
            SpawnFloor(floorIndex,     currentFloorEnemies, true);
            SpawnFloor(floorIndex + 1, nextFloorEnemies,    false);
        }

        public void RegisterEnemy() => enemiesAlive++;

        public void OnEnemyDied()
        {
            enemiesAlive--;
            if (enemiesAlive <= 0)
                OnFloorCleared();
        }

        private void OnFloorCleared()
        {
            foreach (var e in currentFloorEnemies)
            {
                if (!e.activeSelf) continue;
                var enemy = e.GetComponent<EnemyBase>();
                if (enemy != null)
                    ObjectPoolManager.Instance.Return(enemy.PoolPrefab, e);
            }
            currentFloorEnemies.Clear();

            floorIndex++;
            foreach (var e in nextFloorEnemies)
                e.GetComponent<EnemyBase>()?.SetActive(true);
            currentFloorEnemies = nextFloorEnemies;
            nextFloorEnemies = new();

            SpawnFloor(floorIndex + 1, nextFloorEnemies, false);

            GameManager.Instance.OnStageClear();
        }

        private void SpawnFloor(int index, List<GameObject> list, bool active)
        {
            if (index >= floors.Length) return;

            FloorDataSO data = floors[index];
            int spawnIndex = 0;

            foreach (var entry in data.enemies)
            {
                for (int i = 0; i < entry.count; i++)
                {
                    Vector3 pos = floorSpawnPoints[index].position + Vector3.right * spawnIndex * spawnSpacing;
                    GameObject obj = ObjectPoolManager.Instance.Get(entry.prefab, pos, Quaternion.identity);

                    var enemy = obj.GetComponent<EnemyBase>();
                    if (enemy != null)
                    {
                        enemy.PoolPrefab = entry.prefab;
                        enemy.Reset();           // isActive=false, HP 초기화, RegisterEnemy
                        enemy.SetActive(active); // 이후 활성화 여부 설정
                    }

                    list.Add(obj);
                    spawnIndex++;
                }
            }
        }
    }
}
