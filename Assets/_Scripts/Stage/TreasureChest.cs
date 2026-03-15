using UnityEngine;
using System.Collections;
using TowerBreaker.Data;
using TowerBreaker.Equipment;

namespace TowerBreaker.Stage
{
    public class TreasureChest : MonoBehaviour
    {
        [Header("Loot")]
        [SerializeField] private LootTable lootTable;

        [Header("Drop")]
        [SerializeField] private float startOffsetX = 3f;
        [SerializeField] private float startOffsetY = 5f;
        [SerializeField] private float throwForceX = -2f;
        [SerializeField] private float throwForceY = 2f;
        [SerializeField] private float gravity = 15f;
        [SerializeField] private float bounceFactor = 0.3f;
        [SerializeField] private float groundOffsetY = 0f;

        private Animator animator;
        private static readonly int HashOpen = Animator.StringToHash("Open");

        public bool IsDropDone { get; private set; }
        public bool IsOpenDone { get; private set; }
        public ItemDataSO DroppedItem { get; private set; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Drop(Vector3 playerPos)
        {
            IsDropDone = false;
            IsOpenDone = false;
            StartCoroutine(DropRoutine(playerPos));
        }

        private IEnumerator DropRoutine(Vector3 playerPos)
        {
            // 시작 위치: 화면 오른쪽 위
            Vector3 pos = new Vector3(
                playerPos.x + startOffsetX,
                playerPos.y + startOffsetY,
                0f
            );
            transform.position = pos;

            float velX = throwForceX;
            float velY = throwForceY;
            float groundY = playerPos.y + groundOffsetY;

            int bounceCount = 0;

            while (true)
            {
                velY -= gravity * Time.deltaTime;
                pos.x += velX * Time.deltaTime;
                pos.y += velY * Time.deltaTime;

                // 바닥 충돌
                if (pos.y <= groundY)
                {
                    pos.y = groundY;
                    bounceCount++;

                    if (bounceCount >= 2 || Mathf.Abs(velY) < 1f)
                    {
                        transform.position = pos;
                        break;
                    }

                    velY = -velY * bounceFactor;
                    velX *= 0.5f;
                }

                transform.position = pos;
                yield return null;
            }

            IsDropDone = true;
        }

        public void Open()
        {
            DroppedItem = lootTable?.Roll()?.item;
            animator?.SetTrigger(HashOpen);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        // Animation Event: Open 애니메이션 마지막 프레임에서 호출
        public void OnOpenDone()
        {
            IsOpenDone = true;
        }
    }
}
