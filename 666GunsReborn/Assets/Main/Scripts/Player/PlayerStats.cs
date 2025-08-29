using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // 체력
    private int baseHealth;
    public int CurrentHealth;

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
    private float dashCooldown;
    private float currentDashCooldown;

    // 현재 공격 중인지 여부
    public bool IsAttacking = false;

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

        dashCooldown = playerData.dashCooldown;
        currentDashCooldown = 0.0f;

        //방어구 장착 이벤트 등록
        ArmorManager.Instance.onEquipArmor += HandleArmorChanaged;
    }

    #region Health 관련
    public bool DecreaseHealth(int damaage)
    {
        CurrentHealth -= damaage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            // 플레이어 사망 처리
            return true;
        }
        return false;
    }
    #endregion

    #region Dash CoolTime 계산
    public void DashCountCoolDown()
    {
        if (CurrentDashCount <= baseDashCount)
        {
            currentDashCooldown += Time.deltaTime;
            Debug.Log("Dash Cooldown: " + currentDashCooldown);
            if (currentDashCooldown >= dashCooldown)
            {
                Debug.Log($"Dash Count Recovered : {CurrentDashCount}");
                currentDashCooldown = 0;
                CurrentDashCount++;
            }
        }
    }
    #endregion

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