using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public enum SurgeryRoomState
{
    Idle,           // ��� ����
    NPCInteraction, // NPC�� ��ȣ�ۿ�
    SurgerySelect,  // ���� ���� ��
    SurgeryConfirm, // ���� Ȯ�� ��
    SurgeryComplete // ���� �Ϸ�
}

//���� ����
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
    public int _upgradeCost;    //���׷��̵� ���
    public float _statModifier; //���� ������

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
    public Player.PlayerManager _playerManager;

    [Header("Available Surgeries")]
    public List<Surgery> _availableSurgeries;

    [Header("Selected Surgery")]
    private Surgery _selectedSurgery;

    [Header("Surgery History")]
    public Dictionary<SurgeryType, int> _surgeryCount = new Dictionary<SurgeryType, int>();

    private Dictionary<SurgeryType, System.Action<float>> _statModifiers;

    //������ �̺�Ʈ��
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
        //���� ������ ����
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
            Debug.Log($"������ ���� ����: {_currentState}");
        }
    }

    void EnterSurgeryRoom()
    {
        OnChangeState(SurgeryRoomState.NPCInteraction);
        Debug.Log("�����ǿ� �����߽��ϴ�.");
    }

    public bool SelectSurgery(SurgeryType surgeryType)
    {
        if (_currentState != SurgeryRoomState.SurgerySelect)
        {
            Debug.LogWarning("���� �����ǿ��� ������ ������ �� �����ϴ�.");
            return false;
        }

        Surgery surgery = _availableSurgeries.Find(s => s._surgeryType == surgeryType);
        if (surgery == null)
        {
            Debug.LogWarning("������ ������ ��ȿ���� �ʽ��ϴ�.");
            return false;
        }

        _selectedSurgery = surgery;
        OnChangeState(SurgeryRoomState.SurgeryConfirm);
        _surgerySelected?.Invoke(_selectedSurgery);

        Debug.Log($"������ ���õǾ����ϴ�: {_selectedSurgery._surgeryType} ({_selectedSurgery._upgradeCost}) ��");

        return true;
    }

    public bool ConfirmSurgery()
    {
        //���� ó��
        if (_currentState != SurgeryRoomState.SurgeryConfirm)
        {
            Debug.LogWarning("���� ������ Ȯ���� �� �����ϴ�.");
            return false;
        }

        if (_selectedSurgery == null)
        {
            Debug.LogWarning("���õ� ������ �����ϴ�.");
            OnChangeState(SurgeryRoomState.SurgerySelect);
            return false;
        }

        if (_playerManager.GetHoldCoins() < _selectedSurgery._upgradeCost)
        {
            string message = "���� ����� �����մϴ�.";
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
            Debug.LogError("�÷��̾� ������ �Ҵ���� �ʾҽ��ϴ�.");
            OnChangeState(SurgeryRoomState.SurgerySelect);
            return;
        }

        //���� ����
        ApplyStatsModification(_selectedSurgery);

        //��� ����
        _playerManager.MinusHoldCoins(surgery._upgradeCost);

        //���
        _surgeryCount[surgery._surgeryType]++;

        //�Ϸ� ���� ��ȯ
        OnChangeState(SurgeryRoomState.SurgeryComplete);
        _surgeryCompleted?.Invoke(surgery);

        Debug.Log($"������ �Ϸ�Ǿ����ϴ�: {surgery._surgeryType}");
    }

    #endregion

    #region Stat  Modification

    private void ApplyStatsModification(Surgery surgery)
    {
        if(_statModifiers.TryGetValue(surgery._surgeryType, out var modifyAction))
        {
            modifyAction(surgery._statModifier);
            Debug.Log($"{surgery._surgeryType} ������ {surgery._statModifier}��ŭ �����߽��ϴ�.");
        }
        else
        {
            Debug.LogWarning("�ش� ���� Ÿ�Կ� ���� ���� ���� �Լ��� �����ϴ�.");
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

