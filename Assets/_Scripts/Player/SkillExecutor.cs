using System.Collections;
using UnityEngine;
using TowerBreaker.Data;
using TowerBreaker.Equipment;

namespace TowerBreaker.Player
{
    public class SkillExecutor : MonoBehaviour
    {
        private PlayerController controller;
        private InventoryManager inventory;

        private float[] cooldownTimers;

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
            inventory  = GetComponent<InventoryManager>();
        }

        /// <summary>
        /// 슬롯 인덱스(0=무기, 1=방어구, 2=방패)에 해당하는 스킬을 실행한다.
        /// </summary>
        public void ExecuteSkill(int slotIndex)
        {
            // TODO: inventory에서 슬롯별 SkillDataSO 가져오기
            // TODO: 쿨다운 체크 후 스킬 타입에 따라 분기 실행
            // TODO: 쿨다운 타이머 시작
        }

        private IEnumerator CooldownRoutine(int slotIndex, float duration)
        {
            // TODO: duration 동안 cooldownTimers[slotIndex] 카운트다운
            //       완료 시 UI에 쿨다운 종료 이벤트 발행
            float remaining = duration;
            while (remaining > 0f)
            {
                remaining -= Time.deltaTime;
                yield return null;
            }
            // TODO: EventBus.Publish(new SkillReadyEvent { SlotIndex = slotIndex });
        }

        private void CastMeleeSkill(SkillDataSO data)
        {
            // TODO: range 내 적에게 damageMultiplier * baseAtk 데미지 적용
        }

        private void CastProjectileSkill(SkillDataSO data)
        {
            // TODO: projectilePrefab 스폰 후 방향 설정
        }

        private void CastAoESkill(SkillDataSO data)
        {
            // TODO: 플레이어 위치 중심으로 range 내 범위 피해
        }
    }
}
