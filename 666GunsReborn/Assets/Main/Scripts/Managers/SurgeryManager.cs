using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//¼ö¼ú½Ç »óÅÂ
public enum SurgeryRoomState
{
<<<<<<< HEAD
<<<<<<< HEAD
    Idle,
    SurgerySelect,
    SurgeryConfirm,
    SurgeryComplete
}

//¼ö¼ú ½ºÅÈ Å¸ÀÔ
public enum StatType
=======
    Idle,           // ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
    NPCInteraction, // NPCï¿½ï¿½ ï¿½ï¿½È£ï¿½Û¿ï¿½
    SurgerySelect,  // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½
    SurgeryConfirm, // ï¿½ï¿½ï¿½ï¿½ È®ï¿½ï¿½ ï¿½ï¿½
    SurgeryComplete // ï¿½ï¿½ï¿½ï¿½ ï¿½Ï·ï¿½
}

=======
    Idle,           // ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
    NPCInteraction, // NPCï¿½ï¿½ ï¿½ï¿½È£ï¿½Û¿ï¿½
    SurgerySelect,  // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½
    SurgeryConfirm, // ï¿½ï¿½ï¿½ï¿½ È®ï¿½ï¿½ ï¿½ï¿½
    SurgeryComplete // ï¿½ï¿½ï¿½ï¿½ ï¿½Ï·ï¿½
}

>>>>>>> origin/main
//ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
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
    public int _upgradeCost;    //ï¿½ï¿½ï¿½×·ï¿½ï¿½Ìµï¿½ ï¿½ï¿½ï¿½
    public float _statModifier; //ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
<<<<<<< HEAD
>>>>>>> origin/main
=======
>>>>>>> origin/main

    // »ý¼ºÀÚ
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

    //ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ìºï¿½Æ®ï¿½ï¿½
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
        // ÃÊ±â ½ºÅÈ µ¥ÀÌÅÍ ¼³Á¤
        _statData[StatType.Offensive] = new StatTypeData(StatType.Offensive, 100, 1.5f);
        _statData[StatType.Defensive] = new StatTypeData(StatType.Defensive, 100, 1.5f);
        _statData[StatType.Balanced] = new StatTypeData(StatType.Balanced, 100, 1.5f);
=======
=======
>>>>>>> origin/main
        //ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
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
            Debug.Log($"ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½: {_currentState}");
        }
    }

    void EnterSurgeryRoom()
    {
        OnChangeState(SurgeryRoomState.NPCInteraction);
        Debug.Log("ï¿½ï¿½ï¿½ï¿½ï¿½Ç¿ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ß½ï¿½ï¿½Ï´ï¿½.");
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
            Debug.LogWarning("ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ç¿ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½.");
            return false;
        }

        Surgery surgery = _availableSurgeries.Find(s => s._surgeryType == surgeryType);
        if (surgery == null)
        {
            Debug.LogWarning("ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½È¿ï¿½ï¿½ï¿½ï¿½ ï¿½Ê½ï¿½ï¿½Ï´ï¿½.");
            return false;
        }
>>>>>>> origin/main

        int _cost = _statData[_type]._baseCost;

<<<<<<< HEAD
<<<<<<< HEAD
        StateChange(SurgeryRoomState.SurgeryConfirm);
=======
        Debug.Log($"ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ÃµÇ¾ï¿½ï¿½ï¿½ï¿½Ï´ï¿½: {_selectedSurgery._surgeryType} ({_selectedSurgery._upgradeCost}) ï¿½ï¿½");
>>>>>>> origin/main
=======
        Debug.Log($"ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ÃµÇ¾ï¿½ï¿½ï¿½ï¿½Ï´ï¿½: {_selectedSurgery._surgeryType} ({_selectedSurgery._upgradeCost}) ï¿½ï¿½");
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
        //ï¿½ï¿½ï¿½ï¿½ Ã³ï¿½ï¿½
        if (_currentState != SurgeryRoomState.SurgeryConfirm)
        {
            Debug.LogWarning("ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ È®ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½.");
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
            _OnOperationFailed?.Invoke("ºñ¿ë ºÎÁ·.");
            return false;
        }

        ExcuteTypeChange(_selectedType, _cost);
=======
=======
>>>>>>> origin/main
            Debug.LogWarning("ï¿½ï¿½ï¿½Ãµï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½.");
            OnChangeState(SurgeryRoomState.SurgerySelect);
            return false;
        }

        if (_playerManager.GetHoldCoins() < _selectedSurgery._upgradeCost)
        {
            string message = "ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Õ´Ï´ï¿½.";
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
            Debug.LogError("PlayerStatÀÌ ¾ø½À´Ï´Ù.");
            return;
        }

        //º¸³Ê½º °³³ä(¾Æ¸¶ ¾øÀ» µí ½Í´Ù)
        //float _levelBonus = level * 0.1f; // ·¹º§´ç 10% Áõ°¡

        //Å¸ÀÔ º°·Î ½ºÅÝ Àû¿ë
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
            Debug.LogError("ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ò´ï¿½ï¿½ï¿½ï¿½ ï¿½Ê¾Ò½ï¿½ï¿½Ï´ï¿½.");
            OnChangeState(SurgeryRoomState.SurgerySelect);
            return;
        }

        //ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
        ApplyStatsModification(_selectedSurgery);

        //ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
        _playerManager.MinusHoldCoins(surgery._upgradeCost);

        //ï¿½ï¿½ï¿½
        _surgeryCount[surgery._surgeryType]++;

        //ï¿½Ï·ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½È¯
        OnChangeState(SurgeryRoomState.SurgeryComplete);
        _surgeryCompleted?.Invoke(surgery);

        Debug.Log($"ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ï·ï¿½Ç¾ï¿½ï¿½ï¿½ï¿½Ï´ï¿½: {surgery._surgeryType}");
    }

    #endregion

    #region Stat  Modification

    private void ApplyStatsModification(Surgery surgery)
    {
        if(_statModifiers.TryGetValue(surgery._surgeryType, out var modifyAction))
        {
            modifyAction(surgery._statModifier);
            Debug.Log($"{surgery._surgeryType} ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ {surgery._statModifier}ï¿½ï¿½Å­ ï¿½ï¿½ï¿½ï¿½ï¿½ß½ï¿½ï¿½Ï´ï¿½.");
        }
        else
        {
            Debug.LogWarning("ï¿½Ø´ï¿½ ï¿½ï¿½ï¿½ï¿½ Å¸ï¿½Ô¿ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Ô¼ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½.");
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

        Debug.Log("°ø°Ý ½ºÅÝ Àû¿ë.");
    }

    void ApplyDefensiveType(PlayerStat _stat)
    {
        //float _attackBounus = Mathf.Lerp(0.8f, 0.9f, 1f);
        float _defenseBonus = 1.3f;
        float _moveSpeedBonus = 0.9f;
        float _additionalHealthBonus = 50f;

        _stat.baseHealth = Mathf.RoundToInt(_stat.baseHealth * _defenseBonus + _additionalHealthBonus);
        _stat.baseMoveSpeed *= _moveSpeedBonus;


        Debug.Log("¹æ¾î ½ºÅÝ Àû¿ë.");
    }

    void ApplyBalancedType(PlayerStat _stat)
    {
        float _baseBonus = 1.1f;
        float _moveSpeedBonus = 1.0f;

        _stat.baseHealth = Mathf.RoundToInt(_stat.baseHealth * _baseBonus);
        _stat.baseMoveSpeed *= _moveSpeedBonus;

        _stat.baseDashCount = Mathf.RoundToInt(_stat.baseDashCount * _baseBonus);


        Debug.Log("¹ë·±½º ½ºÅÝ Àû¿ë.");
    }

    #endregion
}