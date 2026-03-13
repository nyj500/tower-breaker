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

        public void ExecuteAttack(int comboStep)
        {
            // TODO: hitDetection으로 범위 내 적 탐색
            // TODO: DamageCalculator.Calculate()로 최종 데미지 산출
            // TODO: 적에게 ApplyDamage() 호출
            // TODO: HitFeedback 트리거 (VFX, SFX, 히트스톱)
        }

        public float GetAttackDamage()
        {
            float baseDmg    = controller.stats.attack;
            ItemDataSO weapon = inventory?.GetEquippedWeapon();
            float multiplier  = weapon != null ? weapon.damageMultiplier : 1f;
            return baseDmg * multiplier;
        }

        public float GetDefenseBonus()
        {
            ItemDataSO armor = inventory?.GetEquippedArmor();
            return armor?.defenseBonus ?? 0f;
        }

        public float GetMoveSpeedBonus()
        {
            ItemDataSO shoes = inventory?.GetEquippedShoes();
            return shoes?.moveSpeedBonus ?? 0f;
        }

        public void ApplyBlock()
        {
            // TODO: 방패 → Accessory 장착 아이템 기반 블록 판정 처리
        }
    }
}
