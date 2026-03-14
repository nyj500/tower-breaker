using UnityEngine;
using System.Collections.Generic;
using TowerBreaker.Data;
using TowerBreaker.Equipment;
using TowerBreaker.Enemy;
using TowerBreaker.Player.States;

namespace TowerBreaker.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Hitboxes")]
        [SerializeField]
        private BoxCollider2D attackHitBox;
        [SerializeField]
        private BoxCollider2D skill1HitBox;
        [SerializeField]
        private BoxCollider2D skill2HitBox;
        [SerializeField]
        private BoxCollider2D skill3HitBox;
        [SerializeField] 
        private BoxCollider2D blockHitBox;

        private PlayerController controller;
        private InventoryManager inventory;
        private List<Collider2D> results = new();

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
            inventory = GetComponent<InventoryManager>();
        }

        public float GetAttackDamage()
        {
            float baseDmg = controller.stats.attack;
            ItemDataSO weapon = inventory?.GetEquippedWeapon();
            float multiplier = weapon != null ? weapon.damageMultiplier : 1f;
            return baseDmg * multiplier;
        }

        private BoxCollider2D GetHitBox(IPlayerState state)
        {
            switch (state)
            {
                case AttackState:
                    return attackHitBox;

                case Skill1State:
                    return skill1HitBox;

                case Skill2State:
                    return skill2HitBox;

                case Skill3State:
                    return skill3HitBox;
                case BlockState:
                    return blockHitBox;

                default:
                    return null;
            }
        }

        public void EnableHitBox(IPlayerState state)
        {
            GetHitBox(state).enabled = true;
        }

        public void DisableHitBox(IPlayerState state)
        {
            GetHitBox(state).enabled = false;
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
