using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ ����
public enum SurgeryRoomState
{
<<<<<<< HEAD
<<<<<<< HEAD
    Idle,
    SurgerySelect,
    SurgeryConfirm,
    SurgeryComplete
}

//���� ���� Ÿ��
public enum StatType
=======
    Idle,           // ��� ����
    NPCInteraction, // NPC�� ��ȣ�ۿ�
    SurgerySelect,  // ���� ���� ��
    SurgeryConfirm, // ���� Ȯ�� ��
    SurgeryComplete // ���� �Ϸ�
}

=======
    Idle,           // ��� ����
    NPCInteraction, // NPC�� ��ȣ�ۿ�
    SurgerySelect,  // ���� ���� ��
    SurgeryConfirm, // ���� Ȯ�� ��
    SurgeryComplete // ���� �Ϸ�
}

>>>>>>> origin/main
//���� ����
public enum SurgeryType
>>>>>>> origin/main
{
    Offensive,
    Defensive,
    Balanced
}

[System.Serializable]
public class StatTypeData
{
<<<<<<< HEAD
    public StatType _type;
    public int _level;
    public int _baseCost;
    public float _costMultiplier;
=======
    public SurgeryType _surgeryType;
    public int _upgradeCost;    //���׷��̵� ���
    public float _statModifier; //���� ������
<<<<<<< HEAD
>>>>>>> origin/main
=======
>>>>>>> origin/main

    // ������
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

<<<<<<< HEAD
    [Header("Setting")]
    public PlayerManager _playerManager;
=======
    [Header("Surgery Room State")]
    public SurgeryRoomState _currentState = SurgeryRoomState.Idle;

    [Header("Player Money")]
    public Player.PlayerManager _playerManager;
<<<<<<< HEAD
>>>>>>> origin/main
=======
>>>>>>> origin/main

    [Header("Current State")]
    public SurgeryRoomState _currentState = SurgeryRoomState.Idle;
    public StatType _currentStatType = StatType.Balanced;

<<<<<<< HEAD
    private Dictionary<StatType, StatTypeData> _statData = new Dictionary<StatType, StatTypeData>();
    private StatType _selectedType;
=======
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
>>>>>>> origin/main

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
<<<<<<< HEAD
<<<<<<< HEAD
        // �ʱ� ���� ������ ����
        _statData[StatType.Offensive] = new StatTypeData(StatType.Offensive, 100, 1.5f);
        _statData[StatType.Defensive] = new StatTypeData(StatType.Defensive, 100, 1.5f);
        _statData[StatType.Balanced] = new StatTypeData(StatType.Balanced, 100, 1.5f);
=======
=======
>>>>>>> origin/main
        //���� ������ ����
        _availableSurgeries = new List<Surgery>
        {
            new Surgery(SurgeryType.Health, 100, 20f),
            new Surgery(SurgeryType.MoveSpeed, 150, 0.5f),
            new Surgery(SurgeryType.DashCount, 200, 1f),
            new Surgery(SurgeryType.DashDistance, 250, 1f)
        };
>>>>>>> origin/main
    }

    #region State Management
    void StateChange(SurgeryRoomState _state)
    {
        if(_currentState == _state)
            return;
<<<<<<< HEAD

        _currentState = _state;
    }

    public void EnterSurgeryRoom() => StateChange(SurgeryRoomState.SurgerySelect);
    public void ExitSurgeryRoom() => StateChange(SurgeryRoomState.Idle);
=======
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
>>>>>>> origin/main

    #endregion

    #region Type Change

    public bool SelectTypeChange(StatType _type)
    {
        if(_currentState != SurgeryRoomState.SurgerySelect)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            _OnOperationFailed?.Invoke("Cannot change type in current state.");
            return false;
        }

        _selectedType = _type;
=======
=======
>>>>>>> origin/main
            Debug.LogWarning("���� �����ǿ��� ������ ������ �� �����ϴ�.");
            return false;
        }

        Surgery surgery = _availableSurgeries.Find(s => s._surgeryType == surgeryType);
        if (surgery == null)
        {
            Debug.LogWarning("������ ������ ��ȿ���� �ʽ��ϴ�.");
            return false;
        }
>>>>>>> origin/main

        int _cost = _statData[_type]._baseCost;

<<<<<<< HEAD
<<<<<<< HEAD
        StateChange(SurgeryRoomState.SurgeryConfirm);
=======
        Debug.Log($"������ ���õǾ����ϴ�: {_selectedSurgery._surgeryType} ({_selectedSurgery._upgradeCost}) ��");
>>>>>>> origin/main
=======
        Debug.Log($"������ ���õǾ����ϴ�: {_selectedSurgery._surgeryType} ({_selectedSurgery._upgradeCost}) ��");
>>>>>>> origin/main

        return true;
    }

    public bool ConfirmTypeChange()
    {
<<<<<<< HEAD
<<<<<<< HEAD
        if(_currentState != SurgeryRoomState.SurgeryConfirm)
        {
            _OnOperationFailed?.Invoke("Cannot confirm type change in current state.");
=======
=======
>>>>>>> origin/main
        //���� ó��
        if (_currentState != SurgeryRoomState.SurgeryConfirm)
        {
            Debug.LogWarning("���� ������ Ȯ���� �� �����ϴ�.");
<<<<<<< HEAD
>>>>>>> origin/main
=======
>>>>>>> origin/main
            return false;
        }

        int _cost = _statData[_selectedType]._baseCost;

        if(_playerManager.GetHoldCoins() < _cost)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            _OnOperationFailed?.Invoke("��� ����.");
            return false;
        }

        ExcuteTypeChange(_selectedType, _cost);
=======
=======
>>>>>>> origin/main
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
>>>>>>> origin/main
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
<<<<<<< HEAD
<<<<<<< HEAD
            Debug.LogError("PlayerStat�� �����ϴ�.");
            return;
        }

        //���ʽ� ����(�Ƹ� ���� �� �ʹ�)
        //float _levelBonus = level * 0.1f; // ������ 10% ����

        //Ÿ�� ���� ���� ����
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
=======
=======
>>>>>>> origin/main
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
<<<<<<< HEAD
>>>>>>> origin/main
=======
>>>>>>> origin/main
        }
    }

    void ApplyOffensiveType(PlayerStat _stat)
    {
        //float _attackBounus = 1.3f;
        float _defenseBonus = Mathf.Lerp(0.8f, 0.9f, 1f);
        float _moveSpeedBonus = 1.1f;

        _stat.baseHealth = Mathf.RoundToInt(_stat.baseHealth * _defenseBonus);
        _stat.baseMoveSpeed *= _moveSpeedBonus;

        Debug.Log("���� ���� ����.");
    }

    void ApplyDefensiveType(PlayerStat _stat)
    {
        //float _attackBounus = Mathf.Lerp(0.8f, 0.9f, 1f);
        float _defenseBonus = 1.3f;
        float _moveSpeedBonus = 0.9f;
        float _additionalHealthBonus = 50f;

        _stat.baseHealth = Mathf.RoundToInt(_stat.baseHealth * _defenseBonus + _additionalHealthBonus);
        _stat.baseMoveSpeed *= _moveSpeedBonus;


        Debug.Log("��� ���� ����.");
    }

    void ApplyBalancedType(PlayerStat _stat)
    {
        float _baseBonus = 1.1f;
        float _moveSpeedBonus = 1.0f;

        _stat.baseHealth = Mathf.RoundToInt(_stat.baseHealth * _baseBonus);
        _stat.baseMoveSpeed *= _moveSpeedBonus;

        _stat.baseDashCount = Mathf.RoundToInt(_stat.baseDashCount * _baseBonus);


        Debug.Log("�뷱�� ���� ����.");
    }

    #endregion
}