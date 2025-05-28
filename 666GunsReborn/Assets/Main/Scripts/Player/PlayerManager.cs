using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Dictionary<PlayerType, Type> playerTypeMap = new Dictionary<PlayerType, Type>{
        { PlayerType.Attack, typeof(FormOfAttackPlayer) },
        { PlayerType.Defense, typeof(FormOfDefensePlayer) },
        { PlayerType.Balance, typeof(FormOfBalancePlayer) }
    };

    [Header("Player Type")]
    // 시작할 때 Default 값으로 설정
    private PlayerType playerType = PlayerType.Attack;
    public PlayerType PlayerType {get { return playerType;} private set { }}
    private WeaponType[] equipWeaponType = new WeaponType[2] { WeaponType.Rifle, WeaponType.Rifle };
    public WeaponType[] EquipWeaponType { get { return equipWeaponType; } private set { } }

    [Header("Player Variables")]
    [NonSerialized]
    public Player Currentplayer;

    protected override bool IsPersistent => true;

#region Set Player Info Code
    /// <summary>
    /// Sets the player type.
    /// <summary>
    public void SetPlayerType(PlayerType type)
    {
        playerType = type;
        Debug.Log("Player Type Set: " + playerType);
    }

    /// <summary>
    /// Sets the weapon type for a specific index.
    /// <summary>
    /// param name="type">The weapon type to set.</param>
    /// param name="index">The index in the equipWeaponType array to set.</param>
    /// /// <exception cref="System.ArgumentOutOfRangeException">Thrown when index is out of range.</exception>
    public void SetWeaponType(WeaponType type, int index){
        equipWeaponType[index] = type;
        Debug.Log("Weapon Type Set: " + type + " at index " + index);
    }
    #endregion

    #region Player Initialization Code
    public void InitializePlayer(GameObject playerObject)
    {
        // Player 초기화 로직
        Debug.Log("Player Initialized with Type: " + playerType);

        // 플레이어 초기화
        if (!playerTypeMap.TryGetValue(playerType, out Type type))
        {
            Debug.LogError("Unsupported PlayerType: " + playerType);
            return;
        }

        Component playerScript = playerObject.AddComponent(type);
        if (playerScript is IPlayer player)
            player.Initialized(type);
    }
#endregion
}