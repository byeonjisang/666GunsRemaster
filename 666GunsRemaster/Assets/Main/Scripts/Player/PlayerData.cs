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
    [Tooltip("��� ����")]
    public int dashCount;
    [Tooltip("�뽬 �ӵ�")]
    public float dashSpeed;
    [Tooltip("�뽬 ���� �ð�")]
    public float dashDuration;
    [Tooltip("�뽬 ��Ÿ��")]
    public float dashCooldown;
    [Tooltip("�뽬�� �ٽ� ���� �ð�")]
    public float fillInTime;

    [Header("Player Level")]
    public float maxExp;

    [Header("OverHit")]
    public float overHitTime;
    public float overHitCount;
}