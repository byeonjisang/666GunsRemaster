using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//수술실 상태
public enum SurgeryRoomState
{
    Idle,
    SurgerySelect,
    SurgeryConfirm,
    SurgeryComplete
}

//수술 스탯 타입
public enum StatType
{
    Offensive,
    Defensive,
    Balanced
}

[System.Serializable]
public class StatTypeData
{
    public StatType _type;
    public int _level;
    public int _baseCost;
    public float _costMultiplier;

    // 생성자
    public StatTypeData(StatType type, int baseCost, float costMultiplier)
    {
        _type = type;
        _level = 0;
        _baseCost = baseCost;
        _costMultiplier = costMultiplier;
    }

    public int GetNextLevelCost() => Mathf.CeilToInt(_baseCost * Mathf.Pow(_costMultiplier, _level));
}

public class SurgeryManager : Singleton<SurgeryManager>
{
    protected override bool IsPersistent => true;

    [Header("Setting")]
    public PlayerManager _playerManager;

    [Header("Current State")]
    public SurgeryRoomState _currentState = SurgeryRoomState.Idle;
    public StatType _currentStatType = StatType.Balanced;

    private Dictionary<StatType, StatTypeData> _statData = new Dictionary<StatType, StatTypeData>();
    private StatType _selectedType;

    public event Action<StatType, int> _OnStatTypeChanged;
    public event Action<StatType, int> _OnStatUpgraded;
    public event Action<string> _OnOperationFailed;

    protected override void Awake()
    {
        base.Awake();
        InitializeStatData();
    }

    void InitializeStatData()
    {
        // 초기 스탯 데이터 설정
        _statData[StatType.Offensive] = new StatTypeData(StatType.Offensive, 100, 1.5f);
        _statData[StatType.Defensive] = new StatTypeData(StatType.Defensive, 100, 1.5f);
        _statData[StatType.Balanced] = new StatTypeData(StatType.Balanced, 100, 1.5f);
    }

    #region State Management
    void StateChange(SurgeryRoomState _state)
    {
        if(_currentState == _state)
            return;

        _currentState = _state;
    }

    public void EnterSurgeryRoom() => StateChange(SurgeryRoomState.SurgerySelect);
    public void ExitSurgeryRoom() => StateChange(SurgeryRoomState.Idle);

    #endregion

    #region Type Change

    public bool SelectTypeChange(StatType _type)
    {
        if(_currentState != SurgeryRoomState.SurgerySelect)
        {
            _OnOperationFailed?.Invoke("Cannot change type in current state.");
            return false;
        }

        _selectedType = _type;

        int _cost = _statData[_type]._baseCost;

        StateChange(SurgeryRoomState.SurgeryConfirm);

        return true;
    }

    public bool ConfirmTypeChange()
    {
        if(_currentState != SurgeryRoomState.SurgeryConfirm)
        {
            _OnOperationFailed?.Invoke("Cannot confirm type change in current state.");
            return false;
        }

        int _cost = _statData[_selectedType]._baseCost;

        if(_playerManager.GetHoldCoins() < _cost)
        {
            _OnOperationFailed?.Invoke("비용 부족.");
            return false;
        }

        ExcuteTypeChange(_selectedType, _cost);
        return true;
    }

    void ExcuteTypeChange(StatType _type, int _cost)
    {
        RemoveCurrentTypeEffects();

        _playerManager.MinusHoldCoins(_cost);

        _currentStatType = _type;
        ApplyTypeEffects(_type, 0);

        StateChange(SurgeryRoomState.SurgeryComplete);
        _OnStatTypeChanged?.Invoke(_type, _cost);
    }

    #endregion

    #region Stat Application

    void RemoveCurrentTypeEffects()
    {
        Debug.Log("Removing effects of " + _currentStatType);
    }

    void ApplyTypeEffects(StatType _type, int _level)
    {
        PlayerStat _stat = PlayerStat.Instance;

        if(_stat == null)
        {
            Debug.LogError("PlayerStat이 없습니다.");
            return;
        }

        //보너스 개념(아마 없을 듯 싶다)
        //float _levelBonus = level * 0.1f; // 레벨당 10% 증가

        //타입 별로 스텟 적용
        switch (_type)
        {
            case StatType.Offensive:
                ApplyOffensiveType(_stat);
                Debug.Log("Applied Offensive effects.");
                break;
            case StatType.Defensive:
                ApplyDefensiveType(_stat);
                break;
            case StatType.Balanced:
                ApplyBalancedType(_stat);
                Debug.Log("Applied Balanced effects.");
                break;
        }
    }

    void ApplyOffensiveType(PlayerStat _stat)
    {
        //float _attackBounus = 1.3f;
        float _defenseBonus = Mathf.Lerp(0.8f, 0.9f, 1f);
        float _moveSpeedBonus = 1.1f;

        _stat.baseHealth = Mathf.RoundToInt(_stat.baseHealth * _defenseBonus);
        _stat.baseMoveSpeed *= _moveSpeedBonus;

        Debug.Log("공격 스텟 적용.");
    }

    void ApplyDefensiveType(PlayerStat _stat)
    {
        //float _attackBounus = Mathf.Lerp(0.8f, 0.9f, 1f);
        float _defenseBonus = 1.3f;
        float _moveSpeedBonus = 0.9f;
        float _additionalHealthBonus = 50f;

        _stat.baseHealth = Mathf.RoundToInt(_stat.baseHealth * _defenseBonus + _additionalHealthBonus);
        _stat.baseMoveSpeed *= _moveSpeedBonus;


        Debug.Log("방어 스텟 적용.");
    }

    void ApplyBalancedType(PlayerStat _stat)
    {
        float _baseBonus = 1.1f;
        float _moveSpeedBonus = 1.0f;

        _stat.baseHealth = Mathf.RoundToInt(_stat.baseHealth * _baseBonus);
        _stat.baseMoveSpeed *= _moveSpeedBonus;

        _stat.baseDashCount = Mathf.RoundToInt(_stat.baseDashCount * _baseBonus);


        Debug.Log("밸런스 스텟 적용.");
    }

    #endregion
}