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

        private PlayerController player;

        private void Awake()
        {
            player = FindFirstObjectByType<PlayerController>();
        }

        private void Start()
        {
            attackButton?.onClick.AddListener(() => player.AttackPressed = true);
            dashButton?.onClick.AddListener(() => player.DashPressed = true);
            blockButton?.onClick.AddListener(() => player.BlockPressed = true);
            skill1Button?.onClick.AddListener(() => player.Skill1Pressed = true);
            skill2Button?.onClick.AddListener(() => player.Skill2Pressed = true);
            skill3Button?.onClick.AddListener(() => player.Skill3Pressed = true);
            returnToMainButton?.onClick.AddListener(() => GameManager.Instance.ReturnToLobby());
        }
    }
}
