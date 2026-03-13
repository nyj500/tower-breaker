using UnityEngine;
using TowerBreaker.Data;
using TowerBreaker.Enemy;

namespace TowerBreaker.Core
{
    public class StageManager : MonoBehaviour
    {
        public static StageManager Instance { get; private set; }

        [SerializeField] private StageDataSO[] stageDataList;
        [SerializeField] private EnemySpawner enemySpawner;

        private int currentFloor = 0;
        private int currentWave = 0;
        private int enemiesAlive = 0;

        public int CurrentFloor => currentFloor;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        public void StartStage(int floor)
        {
            // TODO: stageDataList[floor] 로드, currentFloor/Wave 초기화, 첫 웨이브 시작
            currentFloor = floor;
            currentWave = 0;
            StartNextWave();
        }

        private void StartNextWave()
        {
            // TODO: StageDataSO에서 웨이브 데이터 읽어 EnemySpawner에 전달
        }

        public void OnEnemyDied()
        {
            // TODO: enemiesAlive 감소, 0이면 OnWaveClear() 호출
            enemiesAlive--;
            if (enemiesAlive <= 0)
                OnWaveClear();
        }

        private void OnWaveClear()
        {
            // TODO: 마지막 웨이브면 OnFloorClear(), 아니면 다음 웨이브 시작
            currentWave++;
            StageDataSO data = stageDataList[currentFloor];
            if (currentWave >= data.waveCount)
                OnFloorClear();
            else
                StartNextWave();
        }

        private void OnFloorClear()
        {
            // TODO: GameManager.OnStageClear() 호출, 보상 UI 표시
            GameManager.Instance.OnStageClear();
        }

        public void RegisterEnemy()
        {
            // TODO: enemiesAlive 증가
            enemiesAlive++;
        }
    }
}
