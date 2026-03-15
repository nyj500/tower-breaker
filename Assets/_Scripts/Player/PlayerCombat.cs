using UnityEngine;
using TowerBreaker.Data;
using TowerBreaker.Equipment;
using TowerBreaker.Combat;

namespace TowerBreaker.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Skill3 Projectile")]
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Transform firePoint;

        private PlayerController controller;
        private InventoryManager inventory;
        private HitFeedback feedback;

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
            inventory = GetComponent<InventoryManager>();
            feedback = GetComponent<HitFeedback>();
        }

        public float GetAttackDamage()
        {
            float baseDmg = controller.stats.attack;
            ItemDataSO weapon = inventory?.GetEquippedWeapon();
            float multiplier = weapon != null ? weapon.damageMultiplier : 1f;
            return baseDmg * multiplier;
        }

        public void FireProjectile()
        {
            if (projectilePrefab == null) return;
            var obj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            float damage = GetAttackDamage() * GetDamageMultiplier(HitBox.HitType.Skill3);
            obj.Init(damage, controller.IsFacingRight, feedback);
        }

        public float GetDamageMultiplier(HitBox.HitType type)
        {
            switch (type)
            {
                case HitBox.HitType.Attack:
                    return 1f;

                case HitBox.HitType.Skill1:
                    return 3f;

                case HitBox.HitType.Skill2:
                    return 5f;

                case HitBox.HitType.Skill3:
                    return 2f;
            
                default:
                    return 1f;
            }
        }
    }
}
