using System;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Player Type")]
    // 시작할 때 Default 값으로 설정
    private PlayerType playerType = PlayerType.Attack;
    public PlayerType PlayerType {get { return playerType;} private set { }}
    private WeaponType[] equipWeaponType = new WeaponType[2] { WeaponType.Rifle, WeaponType.Rifle };
    public WeaponType[] EquipWeaponType { get { return equipWeaponType; } private set { } }

#region Set Player Info Code
    /// <summary>
    /// Sets the player type.
    /// <summary>
    public void SetPlayerType(PlayerType type){
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
    public void InitializePlayer()
    {
        // Player 초기화 로직
        Debug.Log("Player Initialized with Type: " + playerType);

        // 플레이어 초기화
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        string typeName = playerType.ToString() + "Player";
        Type type = Type.GetType(typeName);

        if (type == null)
        {
            Debug.LogError("Player type not found: " + typeName);
            return;
        }
        //Player playerSciprt = playerObject.AddComponent(type).GetComponent<Player>();
        Component playerScript = playerObject.AddComponent(type);
        //playerScript.
        
    }
#endregion
}