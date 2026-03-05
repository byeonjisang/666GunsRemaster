using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 수술실 상태 (기존 기능 + NPC 상호작용 유지)
public enum SurgeryRoomState
{
    Idle,           // 대기 상태
    NPCInteraction, // NPC와 상호작용 (origin/main에서 가져옴)
    SurgerySelect,  // 수술 선택 중
    SurgeryConfirm, // 수술 확인 중
    SurgeryComplete // 수술 완료
}

// 스탯 타입 (HEAD 버전: 공격/방어/밸런스)
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
    // PlayerManager의 네임스페이스가 Player.PlayerManager인지 확인 필요
    public Character.Player.PlayerManager _playerManager; 

    [Header("Current State")]
    public SurgeryRoomState _currentState = SurgeryRoomState.Idle;
    public StatType _currentStatType = StatType.Balanced; // 현재 플레이어의 타입

    // 데이터 관리용 딕셔너리
    private Dictionary<StatType, StatTypeData> _statData = new Dictionary<StatType, StatTypeData>();
    
    // 선택된 타입 임시 저장
    private StatType _selectedType;

    // 이벤트 (필요한 경우 사용)
    public event Action<SurgeryRoomState> _surgeryRoomStateChanged;
    public event Action<StatType, int> _OnStatTypeChanged;
    public event Action<string> _OnOperationFailed;

    protected override void Awake()
    {
        base.Awake();
        InitializeStatData();
    }

    void InitializeStatData()
    {
        // 초기 데이터 세팅 (HEAD 버전 로직 사용)
        _statData[StatType.Offensive] = new StatTypeData(StatType.Offensive, 100, 1.5f);
        _statData[StatType.Defensive] = new StatTypeData(StatType.Defensive, 100, 1.5f);
        _statData[StatType.Balanced] = new StatTypeData(StatType.Balanced, 100, 1.5f);
    }

    #region State Management
    
    void StateChange(SurgeryRoomState _state)
    {
        if (_currentState == _state)
            return;

        _currentState = _state;
        _surgeryRoomStateChanged?.Invoke(_currentState); // 상태 변경 이벤트 호출 추가
        Debug.Log($"수술실 상태 변경: {_currentState}");
    }

    // 외부 호출용 메서드
    public void EnterSurgeryRoom() => StateChange(SurgeryRoomState.NPCInteraction); // 입장 시 상호작용 상태로
    public void StartSurgerySelect() => StateChange(SurgeryRoomState.SurgerySelect); // 대화 후 선택 화면으로
    public void ExitSurgeryRoom() => StateChange(SurgeryRoomState.Idle);

    #endregion

    #region Type Change (수술 로직)

    // 타입을 선택했을 때 (UI 버튼 등에서 호출)
    public bool SelectTypeChange(StatType _type)
    {
        if (_currentState != SurgeryRoomState.SurgerySelect)
        {
            _OnOperationFailed?.Invoke("현재 상태에서는 타입을 변경할 수 없습니다.");
            return false;
        }

        _selectedType = _type;
        int _cost = _statData[_type]._baseCost;

        Debug.Log($"타입이 선택되었습니다: {_selectedType} (비용: {_cost})");
        
        StateChange(SurgeryRoomState.SurgeryConfirm);
        return true;
    }

    // 최종 확인 버튼을 눌렀을 때
    public bool ConfirmTypeChange()
    {
        if (_currentState != SurgeryRoomState.SurgeryConfirm)
        {
            _OnOperationFailed?.Invoke("확인 단계가 아닙니다.");
            return false;
        }

        int _cost = _statData[_selectedType]._baseCost;

        // 돈이 부족한 경우
        if (_playerManager.GetHoldCoins() < _cost)
        {
            _OnOperationFailed?.Invoke("자금이 부족합니다.");
            Debug.LogWarning("자금이 부족합니다.");
            StateChange(SurgeryRoomState.SurgerySelect); // 다시 선택 화면으로
            return false;
        }

        // 수술 실행
        ExcuteTypeChange(_selectedType, _cost);
        return true;
    }

    void ExcuteTypeChange(StatType _type, int _cost)
    {
        RemoveCurrentTypeEffects(); // 기존 효과 제거 (필요하다면 구현)

        _playerManager.MinusHoldCoins(_cost); // 돈 차감

        _currentStatType = _type;
        ApplyTypeEffects(_type, 0); // 새 효과 적용

        StateChange(SurgeryRoomState.SurgeryComplete);
        _OnStatTypeChanged?.Invoke(_type, _cost);
        
        Debug.Log($"수술 완료! 타입 변경됨: {_type}");
    }

    #endregion

    #region Stat Application (스탯 적용)

    void RemoveCurrentTypeEffects()
    {
        // 기존 타입의 버프를 해제하는 로직이 필요하다면 여기에 작성
        Debug.Log("Removing effects of " + _currentStatType);
    }

    void ApplyTypeEffects(StatType _type, int _level)
    {
        PlayerStat _stat = PlayerStat.Instance;

        if (_stat == null)
        {
            Debug.LogError("PlayerStat 인스턴스를 찾을 수 없습니다.");
            return;
        }

        // 타입별 로직 적용 (HEAD 버전)
        switch (_type)
        {
            case StatType.Offensive:
                ApplyOffensiveType(_stat);
                break;
            case StatType.Defensive:
                ApplyDefensiveType(_stat);
                break;
            case StatType.Balanced:
                ApplyBalancedType(_stat);
                break;
        }
    }

    void ApplyOffensiveType(PlayerStat _stat)
    {
        // 예시 로직
        // float _attackBounus = 1.3f;
        float _defenseBonus = Mathf.Lerp(0.8f, 0.9f, 1f); 
        float _moveSpeedBonus = 1.1f;

        // 체력은 좀 줄이고 이속 증가
        _stat.baseHealth = Mathf.RoundToInt(_stat.baseHealth * _defenseBonus);
        _stat.baseMoveSpeed *= _moveSpeedBonus;

        Debug.Log("공격형 타입 적용 완료.");
    }

    void ApplyDefensiveType(PlayerStat _stat)
    {
        float _defenseBonus = 1.3f;
        float _moveSpeedBonus = 0.9f;
        float _additionalHealthBonus = 50f;

        // 체력 대폭 증가, 이속 감소
        _stat.baseHealth = Mathf.RoundToInt(_stat.baseHealth * _defenseBonus + _additionalHealthBonus);
        _stat.baseMoveSpeed *= _moveSpeedBonus;

        Debug.Log("방어형 타입 적용 완료.");
    }

    void ApplyBalancedType(PlayerStat _stat)
    {
        float _baseBonus = 1.1f;
        float _moveSpeedBonus = 1.0f;

        // 골고루 증가
        _stat.baseHealth = Mathf.RoundToInt(_stat.baseHealth * _baseBonus);
        _stat.baseMoveSpeed *= _moveSpeedBonus;
        _stat.baseDashCount = Mathf.RoundToInt(_stat.baseDashCount * _baseBonus);

        Debug.Log("밸런스형 타입 적용 완료.");
    }

    #endregion
}