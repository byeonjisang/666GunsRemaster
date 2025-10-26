using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public enum SurgeryRoomState
{
    Idle,           // 대기 상태
    NPCInteraction, // NPC와 상호작용
    SurgerySelect,  // 수술 선택 중
    SurgeryConfirm, // 수술 확인 중
    SurgeryComplete // 수술 완료
}

//수술 종류
public enum SurgeryType
{
    Health,
    MoveSpeed,
    DashCount,
    DashDistance
}

[System.Serializable]
public class Surgery
{
    public SurgeryType _surgeryType;
    public int _upgradeCost;    //업그레이드 비용
    public float _statModifier; //스텟 증가량

    public Surgery(SurgeryType surgeryType, int upgradeCost, float statModifier)
    {
        this._surgeryType = surgeryType;
        this._upgradeCost = upgradeCost;
        this._statModifier = statModifier;
    }
}
public class SurgeryManager : Singleton<SurgeryManager>
{
    protected override bool IsPersistent => true;

    [Header("Surgery Room State")]
    public SurgeryRoomState _currentState = SurgeryRoomState.Idle;

    [Header("Player Money")]
    public PlayerManager _playerManager;

    [Header("Available Surgeries")]
    public List<Surgery> _availableSurgeries;

    [Header("Selected Surgery")]
    private Surgery _selectedSurgery;

    [Header("Surgery History")]
    public Dictionary<SurgeryType, int> _surgeryCount = new Dictionary<SurgeryType, int>();

    private Dictionary<SurgeryType, System.Action<float>> _statModifiers;

    //수술실 이벤트들
    public event Action<SurgeryRoomState> _surgeryRoomStateChanged;
    public event Action<Surgery> _surgerySelected;
    public event Action<Surgery> _surgeryCompleted;
    public event Action<string> _surgeryFailed;


    protected override void Awake()
    {
        base.Awake();

        InitializeSurgery();
        InitializeSurgeryCount();

        InitializeStatModifiers();
    }

    void InitializeSurgery()
    {
        //스텟 증가량 설정
        _availableSurgeries = new List<Surgery>
        {
            new Surgery(SurgeryType.Health, 100, 20f),
            new Surgery(SurgeryType.MoveSpeed, 150, 0.5f),
            new Surgery(SurgeryType.DashCount, 200, 1f),
            new Surgery(SurgeryType.DashDistance, 250, 1f)
        };
    }

    void InitializeSurgeryCount()
    {
        foreach(SurgeryType type in Enum.GetValues(typeof(SurgeryType)))
        {
            _surgeryCount[type] = 0;
        }
    }

    void InitializeStatModifiers()
    {
        _statModifiers = new Dictionary<SurgeryType, System.Action<float>>
        {
            { SurgeryType.Health, (modifier) => { PlayerStat.Instance.baseHealth += (int)modifier; PlayerStat.Instance.CurrentHealth += (int)modifier; } },
            { SurgeryType.MoveSpeed, (modifier) => { PlayerStat.Instance.baseMoveSpeed += modifier; PlayerStat.Instance.CurrentMoveSpeed += modifier; } },
            { SurgeryType.DashCount, (modifier) => { PlayerStat.Instance.baseDashCount += (int)modifier; PlayerStat.Instance.CurrentDashCount += (int)modifier; } },
            { SurgeryType.DashDistance, (modifier) => { PlayerStat.Instance.baseDashDistance += modifier; PlayerStat.Instance.CurrentDashDistance += modifier; } }
        };
    }


    #region FSM State

    private void OnChangeState(SurgeryRoomState newState)
    {
        if(_currentState == newState)
        {
            return;
        }
        else
        {
            _currentState = newState;
            _surgeryRoomStateChanged?.Invoke(_currentState);
            Debug.Log($"수술실 상태 변경: {_currentState}");
        }
    }

    void EnterSurgeryRoom()
    {
        OnChangeState(SurgeryRoomState.NPCInteraction);
        Debug.Log("수술실에 입장했습니다.");
    }

    public bool SelectSurgery(SurgeryType surgeryType)
    {
        if (_currentState != SurgeryRoomState.SurgerySelect)
        {
            Debug.LogWarning("현재 수술실에서 수술을 선택할 수 없습니다.");
            return false;
        }

        Surgery surgery = _availableSurgeries.Find(s => s._surgeryType == surgeryType);
        if (surgery == null)
        {
            Debug.LogWarning("선택한 수술이 유효하지 않습니다.");
            return false;
        }

        _selectedSurgery = surgery;
        OnChangeState(SurgeryRoomState.SurgeryConfirm);
        _surgerySelected?.Invoke(_selectedSurgery);

        Debug.Log($"수술이 선택되었습니다: {_selectedSurgery._surgeryType} ({_selectedSurgery._upgradeCost}) 원");

        return true;
    }

    public bool ConfirmSurgery()
    {
        //예외 처리
        if (_currentState != SurgeryRoomState.SurgeryConfirm)
        {
            Debug.LogWarning("현재 수술을 확인할 수 없습니다.");
            return false;
        }

        if (_selectedSurgery == null)
        {
            Debug.LogWarning("선택된 수술이 없습니다.");
            OnChangeState(SurgeryRoomState.SurgerySelect);
            return false;
        }

        if (_playerManager.GetHoldCoins() < _selectedSurgery._upgradeCost)
        {
            string message = "수술 비용이 부족합니다.";
            _surgeryFailed?.Invoke(message);
            Debug.LogWarning(message);
            return false;
        }


        ExcuteSurgery(_selectedSurgery);
        return true;
    }

    private void ExcuteSurgery(Surgery surgery)
    {
        if (PlayerStat.Instance == null)
        {
            Debug.LogError("플레이어 스탯이 할당되지 않았습니다.");
            OnChangeState(SurgeryRoomState.SurgerySelect);
            return;
        }

        //스텟 설정
        ApplyStatsModification(_selectedSurgery);

        //비용 차감
        _playerManager.MinusHoldCoins(surgery._upgradeCost);

        //기록
        _surgeryCount[surgery._surgeryType]++;

        //완료 상태 전환
        OnChangeState(SurgeryRoomState.SurgeryComplete);
        _surgeryCompleted?.Invoke(surgery);

        Debug.Log($"수술이 완료되었습니다: {surgery._surgeryType}");
    }

    #endregion

    #region Stat  Modification

    private void ApplyStatsModification(Surgery surgery)
    {
        if(_statModifiers.TryGetValue(surgery._surgeryType, out var modifyAction))
        {
            modifyAction(surgery._statModifier);
            Debug.Log($"{surgery._surgeryType} 스탯이 {surgery._statModifier}만큼 증가했습니다.");
        }
        else
        {
            Debug.LogWarning("해당 수술 타입에 대한 스탯 수정 함수가 없습니다.");
        }
    }

    #endregion

    #region Getter

    public List<Surgery> GetAvailableSurgeries()
    {
        return new List<Surgery>(_availableSurgeries);
    }

    #endregion
}

