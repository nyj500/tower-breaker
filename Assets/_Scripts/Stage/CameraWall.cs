using UnityEngine;

namespace TowerBreaker.Stage
{
    /// <summary>
    /// 카메라 왼쪽 경계에 붙어 이동하는 물리 벽.
    /// BoxCollider2D를 가진 오브젝트에 부착.
    /// </summary>
    public class CameraWall : MonoBehaviour
    {
        [SerializeField] private float offsetX = -1f;

        private Camera mainCamera;
        private BoxCollider2D col;

        private void Awake()
        {
            mainCamera = Camera.main;
            col = GetComponent<BoxCollider2D>();
        }

        private void LateUpdate()
        {
            float leftBound = mainCamera.ViewportToWorldPoint(Vector3.zero).x;
            transform.position = new Vector3(leftBound + offsetX, transform.position.y, 0f);
        }
    }
}
