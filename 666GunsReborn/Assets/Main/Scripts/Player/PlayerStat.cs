using System.Collections;
using UnityEngine;

public class PlayerStat : Singleton<PlayerStat>
{

    /// <summary>
    /// 플레이어의 스텟은 수술실에서 영구적인 변경을 위해 public으로 설정
    /// </summary>

    #region Health, MoveSpeed, DashCount, DashDistance Get Set
    // 체력
    public int baseHealth;
    public int CurrentHealth;
    //public int GetBaseHealth() => baseHealth;
    //public void MinusHealth(int val) => baseHealth -= val;
    //public void PlusHealth(int val) => baseHealth += val;

    // 이동속도
    public float baseMoveSpeed;
    public float CurrentMoveSpeed;
    //public float GetBaseMoveSpeed() => baseMoveSpeed;
    //public void MinusBaseMoveSpeed(int val) => baseMoveSpeed -= val;
    //public void PlusBaseMoveSpeed(int val) => baseMoveSpeed += val;

    // 대쉬
    public int baseDashCount;
    public int CurrentDashCount;
    //public int GetDashCount() => baseDashCount;
    //public void MinusDashCount(int val) => baseDashCount -= val;
    //public void PlusDashCount(int val) => baseDashCount += val;

    // 대쉬 거리
    public float baseDashDistance;
    public float CurrentDashDistance;
    //public float GetDashDistance() => baseDashDistance;
    //public void MinusDashDistance(int val) => baseDashDistance -= val;
    //public void PlusDashDistance(int val) => baseDashDistance += val;



    // 대쉬 쿨타임
    public float dashCooldown;
    private float currentDashCooldown;
    //public float GetDashCooldown() => dashCooldown;
    //public void MinusDashCooldown(int val) => dashCooldown -= val;
    //public void PlusDashCooldown(int val) => dashCooldown += val;

    #endregion

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