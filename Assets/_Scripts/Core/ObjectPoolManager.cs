using System.Collections.Generic;
using UnityEngine;

namespace TowerBreaker.Core
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance { get; private set; }

        private Dictionary<GameObject, Queue<GameObject>> pools = new();

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!pools.ContainsKey(prefab))
                pools[prefab] = new Queue<GameObject>();

            var queue = pools[prefab];
            var obj = queue.Count > 0 ? queue.Dequeue() : Instantiate(prefab, transform);

            obj.transform.SetPositionAndRotation(position, rotation);
            obj.SetActive(true);
            return obj;
        }

        public void Return(GameObject prefab, GameObject obj)
        {
            obj.SetActive(false);
            if (!pools.ContainsKey(prefab))
                pools[prefab] = new Queue<GameObject>();
            pools[prefab].Enqueue(obj);
        }
    }
}
