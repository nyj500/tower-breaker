using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using TowerBreaker.Core;
using TowerBreaker.Data;
using TowerBreaker.Enemy;
using TowerBreaker.Equipment;

namespace TowerBreaker.Stage
{
    public class FloorManager : MonoBehaviour
    {
        public static FloorManager Instance { get; private set; }

        [Header("Floors")]
        [SerializeField] private FloorDataSO[] floors;

        [Header("Spawn")]
        [SerializeField] private Transform[] floorSpawnPoints;
        [SerializeField] private float spawnSpacing = 1f;

        [Header("Floor Clear")]
        [SerializeField] private TreasureChest treasureChest;
        [SerializeField] private GameObject touchUI;
        [SerializeField] private TowerBreaker.UI.ItemDropUI itemDropUI;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float cameraMoveDuration = 0.5f;
        [SerializeField] private float nextFloorDelay = 1f;

        private int floorIndex = 0;
        private bool waitingForTouch;

        public int CurrentFloor => floorIndex;

        public List<GameObject> GetCurrentFloorEnemies() => currentFloorEnemies;

        private List<GameObject> currentFloorEnemies = new();
        private List<GameObject> nextFloorEnemies = new();

        private Camera mainCamera;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            mainCamera = Camera.main;
        }

        private void Start()
        {
            if (touchUI != null) touchUI.SetActive(false);
            if (treasureChest != null) treasureChest.gameObject.SetActive(false);
            SpawnFloor(floorIndex, currentFloorEnemies, true);
            SpawnFloor(floorIndex + 1, nextFloorEnemies, false);
        }

        private void Update()
        {
            if (waitingForTouch && (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame
                || Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame))
            {
                waitingForTouch = false;
            }
        }

        public void OnEnemyDied()
        {
            if (IsFloorCleared())
                StartCoroutine(FloorClearSequence());
        }

        private bool IsFloorCleared()
        {
            if (currentFloorEnemies.Count == 0) return false;

            foreach (var e in currentFloorEnemies)
            {
                var enemy = e.GetComponent<EnemyBase>();
                if (enemy != null && !enemy.IsDead) return false;
            }
            return true;
        }

        private IEnumerator FloorClearSequence()
        {
            // 1. 잔여 적 정리 (이미 죽은 적은 OnDeathAnimationEnd에서 자체 반환)
            foreach (var e in currentFloorEnemies)
            {
                if (!e.activeSelf) continue;
                var enemy = e.GetComponent<EnemyBase>();
                if (enemy == null || enemy.IsDead) continue;
                ObjectPoolManager.Instance.Return(enemy.PoolPrefab, e);
            }
            currentFloorEnemies.Clear();

            // 2. 보물상자 드랍 애니메이션
            if (treasureChest != null)
            {
                treasureChest.gameObject.SetActive(true);
                treasureChest.Drop(playerTransform.position);
                yield return new WaitUntil(() => treasureChest.IsDropDone);
            }

            // 3. "Touch" UI 표시 → 터치 대기 → 상자 열기
            ShowTouchUI();
            yield return WaitForTouch();
            HideTouchUI();

            if (treasureChest != null)
            {
                treasureChest.Open();
                yield return new WaitUntil(() => treasureChest.IsOpenDone);
            }

            // 4. 아이템 인벤토리 추가 + UI 표시 + "Touch" UI → 터치 대기 → 다음 층 이동
            var droppedItem = treasureChest?.DroppedItem;
            PlayerInventory.Instance?.AddItem(droppedItem);
            itemDropUI?.Show(droppedItem);
            ShowTouchUI();
            yield return WaitForTouch();
            HideTouchUI();
            itemDropUI?.Hide();

            if (treasureChest != null)
                treasureChest.Hide();

            // 4층 클리어 시 메인씬으로
            if (floorIndex >= 3)
            {
                GameManager.Instance.ReturnToLobby();
                yield break;
            }

            // 5. 카메라 이동 전에 그 다음 층 미리 스폰 (대기 상태)
            floorIndex++;
            var upcomingEnemies = new List<GameObject>();
            SpawnFloor(floorIndex + 1, upcomingEnemies, false);

            // 6. 카메라 + 플레이어 위로 이동
            yield return MoveToNextFloor();

            // 7. 대기 후 다음 층 적 활성화
            yield return new WaitForSeconds(nextFloorDelay);

            foreach (var e in nextFloorEnemies)
                e.GetComponent<EnemyBase>()?.SetActive(true);
            currentFloorEnemies = nextFloorEnemies;
            nextFloorEnemies = upcomingEnemies;

        }

        private IEnumerator MoveToNextFloor()
        {
            if (floorIndex >= floorSpawnPoints.Length) yield break;

            Vector3 targetFloorPos = floorSpawnPoints[floorIndex].position;

            // 플레이어 순간이동
            if (playerTransform != null)
            {
                var playerPos = playerTransform.position;
                playerTransform.position = new Vector3(-3f, targetFloorPos.y, playerPos.z);
            }

            // 카메라 부드럽게 이동
            Vector3 camStart = mainCamera.transform.position;
            Vector3 camEnd = new Vector3(camStart.x, targetFloorPos.y, camStart.z);

            float elapsed = 0f;
            while (elapsed < cameraMoveDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.SmoothStep(0f, 1f, elapsed / cameraMoveDuration);
                mainCamera.transform.position = Vector3.Lerp(camStart, camEnd, t);
                yield return null;
            }
            mainCamera.transform.position = camEnd;
        }

        private void ShowTouchUI()
        {
            if (touchUI != null) touchUI.SetActive(true);
            waitingForTouch = true;
        }

        private void HideTouchUI()
        {
            if (touchUI != null) touchUI.SetActive(false);
        }

        private IEnumerator WaitForTouch()
        {
            // 한 프레임 대기 (현재 프레임 입력 무시)
            yield return null;
            waitingForTouch = true;
            yield return new WaitUntil(() => !waitingForTouch);
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
                        enemy.Reset();
                        enemy.SetActive(active);
                    }

                    list.Add(obj);
                    spawnIndex++;
                }
            }
        }
    }
}
