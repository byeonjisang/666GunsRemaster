using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public int playerId;

    [Space(5)]
    [Header("Player Stats")]
    public float maxHealth;
    public int maxShield;

    [Header("Player Movement")]
    public float moveSpeed;
    [Tooltip("대수 개수")]
    public int dashCount;
    [Tooltip("대쉬 속도")]
    public float dashSpeed;
    [Tooltip("대쉬 지속 시간")]
    public float dashDuration;
    [Tooltip("대쉬 쿨타임")]
    public float dashCooldown;
    [Tooltip("대쉬가 다시 차는 시간")]
    public float fillInTime;

    [Header("Player Level")]
    public float maxExp;

    [Header("OverHit")]
    public float overHitTime;
    public float overHitCount;
}