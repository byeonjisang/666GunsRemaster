using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{

    public int playerId;
    [Header("Player Stats")]
    public int maxHealth;
    public int maxShield;
    [Header("Player Movement")]
    public float moveSpeed;
    public int dashCount;
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    [Header("Player Level")]
    public float maxExp;
}