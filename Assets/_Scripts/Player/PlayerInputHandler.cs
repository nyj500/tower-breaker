using UnityEngine;

namespace TowerBreaker.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerController controller;

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
        }

        private void Update()
        {
            // 공격 (Z키 또는 마우스 좌클릭)
            controller.AttackPressed = Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0);

            // 대시 (X키 또는 Shift)
            controller.DashPressed = Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift);

            // 블록 (C키)
            controller.BlockPressed = Input.GetKeyDown(KeyCode.C);

            // 스킬 1, 2, 3 (1, 2, 3 키)
            controller.Skill1Pressed = Input.GetKeyDown(KeyCode.Alpha1);
            controller.Skill2Pressed = Input.GetKeyDown(KeyCode.Alpha2);
            controller.Skill3Pressed = Input.GetKeyDown(KeyCode.Alpha3);

            // 방향 전환 (좌우 화살표 또는 A/D)
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                controller.SetFacingDirection(false);
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                controller.SetFacingDirection(true);
        }
    }
}
