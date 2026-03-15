using UnityEngine;

namespace TowerBreaker.FeedbackFX
{
    public class AutoDisable : MonoBehaviour
    {
        // 애니메이션 이벤트에서 호출
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
