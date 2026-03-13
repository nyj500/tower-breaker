using System.Collections.Generic;
using UnityEngine;

namespace TowerBreaker.Core
{
    [System.Serializable]
    public class PoolEntry
    {
        public string key;
        public GameObject prefab;
        public int initialSize;
    }

    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance { get; private set; }

        [SerializeField] private List<PoolEntry> poolEntries;

        private Dictionary<string, Queue<GameObject>> pools = new();
        private Dictionary<string, GameObject> prefabLookup = new();

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePools();
        }

        private void InitializePools()
        {
            // TODO: poolEntries 순회하며 각 풀을 초기 사이즈만큼 미리 생성하여 Queue에 삽입
            foreach (var entry in poolEntries)
            {
                var queue = new Queue<GameObject>();
                prefabLookup[entry.key] = entry.prefab;
                for (int i = 0; i < entry.initialSize; i++)
                {
                    var obj = CreateNew(entry.prefab);
                    queue.Enqueue(obj);
                }
                pools[entry.key] = queue;
            }
        }

        public GameObject Get(string key, Vector3 position, Quaternion rotation)
        {
            // TODO: 풀에서 꺼내거나 부족하면 새로 생성, 위치/회전 설정 후 활성화
            if (!pools.ContainsKey(key))
            {
                Debug.LogWarning($"Pool key '{key}' not found.");
                return null;
            }

            GameObject obj = pools[key].Count > 0
                ? pools[key].Dequeue()
                : CreateNew(prefabLookup[key]);

            obj.transform.SetPositionAndRotation(position, rotation);
            obj.SetActive(true);
            return obj;
        }

        public void Return(string key, GameObject obj)
        {
            // TODO: 오브젝트 비활성화 후 해당 키의 Queue에 반환
            obj.SetActive(false);
            if (pools.ContainsKey(key))
                pools[key].Enqueue(obj);
            else
                Destroy(obj);
        }

        private GameObject CreateNew(GameObject prefab)
        {
            var obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            return obj;
        }
    }
}
