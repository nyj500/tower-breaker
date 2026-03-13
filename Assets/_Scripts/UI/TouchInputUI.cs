using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TowerBreaker.Player;

namespace TowerBreaker.UI
{
    public class TouchInputUI : MonoBehaviour
    {
        [Header("Joystick")]
        [SerializeField] private RectTransform joystickBackground;
        [SerializeField] private RectTransform joystickHandle;
        [SerializeField] private float joystickRadius = 80f;

        [Header("Action Buttons")]
        [SerializeField] private Button attackButton;
        [SerializeField] private Button dashButton;
        [SerializeField] private Button blockButton;

        private PlayerController player;
        private Vector2 joystickInput;
        private int joystickPointerID = -1;

        private void Awake()
        {
            player = FindFirstObjectByType<PlayerController>();
        }

        private void Start()
        {
            // TODO: 버튼 이벤트 등록 (PointerDown/Up 기반 홀드 지원)
            attackButton?.onClick.AddListener(() => player.AttackPressed = true);
            dashButton?.onClick.AddListener(()   => player.DashPressed   = true);
            blockButton?.onClick.AddListener(()  => player.BlockPressed  = true);
        }

        private void Update()
        {
            // TODO: 조이스틱 입력을 player.MoveInput에 반영
            player.MoveInput = joystickInput;

            // TODO: 버튼 플래그 리셋 (1프레임 이후)
            player.AttackPressed = false;
            player.DashPressed   = false;
        }

        // ── 조이스틱 이벤트 (EventTrigger 또는 IPointerDown/Up/Drag 구현) ──

        public void OnJoystickDown(BaseEventData data)
        {
            // TODO: PointerEventData 캐스팅 후 joystickPointerID 저장
        }

        public void OnJoystickDrag(BaseEventData data)
        {
            // TODO: 핸들 위치 계산, joystickRadius 클램프 후 joystickInput 갱신
        }

        public void OnJoystickUp(BaseEventData data)
        {
            // TODO: 핸들을 중앙으로 복귀, joystickInput = Vector2.zero
            joystickInput = Vector2.zero;
            if (joystickHandle != null)
                joystickHandle.anchoredPosition = Vector2.zero;
        }
    }
}
