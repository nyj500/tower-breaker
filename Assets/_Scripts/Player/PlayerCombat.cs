using UnityEngine;
using TowerBreaker.Combat;
using TowerBreaker.Data;
using TowerBreaker.Equipment;

namespace TowerBreaker.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private HitDetection hitDetection;
        [SerializeField] private LayerMask enemyLayer;

        private PlayerController controller;
        private InventoryManager inventory;

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
            inventory  = GetComponent<InventoryManager>();
        }

        /// <summary>
        /// 현재 콤보 단계에 맞는 공격 판정을 발동한다.
        /// </summary>
        public void ExecuteAttack(int comboStep)
        {
            // TODO: hitDetection으로 범위 내 적 탐색
            // TODO: DamageCalculator.Calculate()로 최종 데미지 산출
            // TODO: 적에게 ApplyDamage() 호출
            // TODO: HitFeedback 트리거 (VFX, SFX, 히트스톱)
        }

        public float GetAttackDamage()
        {
            // TODO: stats.attack + 장착 무기의 damageMultiplier 합산 반환
            float baseDmg = controller.stats.attack;
            WeaponDataSO weapon = inventory?.GetEquippedWeapon();
            float multiplier = weapon != null ? weapon.damageMultiplier : 1f;
            return baseDmg * multiplier;
        }

        public void ApplyBlock()
        {
            // TODO: 방패 데이터 읽어 블록 판정 처리 (blockWindowDuration 내 피격 감소)
        }
    }
}
