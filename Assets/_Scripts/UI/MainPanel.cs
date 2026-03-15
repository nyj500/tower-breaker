using UnityEngine;
using UnityEngine.UI;
using TowerBreaker.Core;

namespace TowerBreaker.UI
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField] private Button goToWorldButton;
        [SerializeField] private Button inventoryButton;
        [SerializeField] private InventoryPanel inventoryPanel;

        [Header("SFX")]
        [SerializeField] private AudioClip buttonSFX;

        private void Start()
        {
            goToWorldButton?.onClick.AddListener(() => { PlayButtonSFX(); GameManager.Instance.GoToWorld(); });
            inventoryButton?.onClick.AddListener(() => { PlayButtonSFX(); inventoryPanel?.Show(); });
        }

        private void PlayButtonSFX() => SoundManager.Instance?.PlaySFX(buttonSFX);
    }
}
