using UnityEngine;
using UnityEngine.UI;
using TowerBreaker.Player;
using TowerBreaker.Core;

namespace TowerBreaker.UI
{
    public class GamePanel : MonoBehaviour
    {
        [Header("Action Buttons")]
        [SerializeField] private Button attackButton;
        [SerializeField] private Button dashButton;
        [SerializeField] private Button blockButton;

        [Header("Skill Buttons")]
        [SerializeField] private Button skill1Button;
        [SerializeField] private Button skill2Button;
        [SerializeField] private Button skill3Button;

        [Header("Navigation")]
        [SerializeField] private Button returnToMainButton;

        [Header("SFX")]
        [SerializeField] private AudioClip buttonSFX;

        private PlayerController player;

        private void Awake()
        {
            player = FindFirstObjectByType<PlayerController>();
        }

        private void Start()
        {
            attackButton?.onClick.AddListener(() => { PlayButtonSFX(); player.AttackPressed = true; });
            dashButton?.onClick.AddListener(() => { PlayButtonSFX(); player.DashPressed = true; });
            blockButton?.onClick.AddListener(() => { PlayButtonSFX(); player.BlockPressed = true; });
            skill1Button?.onClick.AddListener(() => { PlayButtonSFX(); player.Skill1Pressed = true; });
            skill2Button?.onClick.AddListener(() => { PlayButtonSFX(); player.Skill2Pressed = true; });
            skill3Button?.onClick.AddListener(() => { PlayButtonSFX(); player.Skill3Pressed = true; });
            returnToMainButton?.onClick.AddListener(() => { PlayButtonSFX(); GameManager.Instance.ReturnToLobby(); });
        }

        private void PlayButtonSFX() => SoundManager.Instance?.PlaySFX(buttonSFX);
    }
}
