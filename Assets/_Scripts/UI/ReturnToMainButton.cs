using UnityEngine;
using UnityEngine.UI;
using TowerBreaker.Core;

namespace TowerBreaker.UI
{
    [RequireComponent(typeof(Button))]
    public class ReturnToMainButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            GameManager.Instance.ReturnToLobby();
        }
    }
}
