using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // 체력
    private float baseHealth;
    public float CurrentHealth;

    // 이동속도
    private float baseMoveSpeed;
    public float CurrentMoveSpeed;

    // 대쉬
    private int baseDashCount;
    public int CurrentDashCount;

    // 대쉬 거리
    private float baseDashDistance;
    public float CurrentDashDistance;

    // 대쉬 쿨타임
    public float DashCooldown;
    public float CurrentDashCooldown;

    // 현재 공격 중인지 여부
    public bool isAttacking = false;

    public void Init(PlayerData playerData)
    {
        baseHealth = playerData.health;
        CurrentHealth = baseHealth;

        baseMoveSpeed = playerData.moveSpeed;
        CurrentMoveSpeed = baseMoveSpeed;

        baseDashCount = playerData.dashCount;
        CurrentDashCount = baseDashCount;

        baseDashDistance = playerData.dashDistance;
        CurrentDashDistance = baseDashDistance;

        DashCooldown = playerData.dashCooldown;
        CurrentDashCooldown = 0.0f;

        //방어구 장착 이벤트 등록
        ArmorManager.Instance.onEquipArmor += HandleArmorChanaged;
    }

    public void DashCountCoolDown()
    {
        if (CurrentDashCount <= baseDashCount)
        {
            CurrentDashCooldown += Time.deltaTime;
            if (CurrentDashCooldown >= DashCooldown)
            {
                CurrentDashCooldown = 0;
                CurrentDashCount++;
            }
        }
    }

    #region Equip Armor
    //장비 착용 시 실행
    private void HandleArmorChanaged()
    {
        UpdateArmorStats();
    }

    private void UpdateArmorStats()
    {
        CurrentHealth = baseHealth;
        CurrentMoveSpeed = baseMoveSpeed;
        CurrentDashDistance = baseDashDistance;

        foreach (Armor armor in ArmorManager.Instance.EquipArmor)
        {
            if (armor == null)
                continue;

            CurrentHealth += armor.HealthValue;
            CurrentMoveSpeed += armor.SpeedValue;
        }
    }
    #endregion
}