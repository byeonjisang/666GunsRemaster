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
    public PlayerType PlayerType { get { return playerType; } private set { } }

    [Header("Player Variables")]
    [NonSerialized]
    public Player Currentplayer;

    private int holdCoins = 0;
    public int HoldCoins { get { return holdCoins; } private set { } }

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
    #endregion

    #region Player Initialization Code
    public void InitializePlayer(GameObject playerObject)
    {
        // Player 초기화 로직
        Debug.Log("Player Initialized with Type: " + playerType);

        holdCoins = 0;
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

    public void AddCoins(int coins)
    {
        holdCoins += coins;
    }
}