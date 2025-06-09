using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Datas/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public int health;
    public float moveSpeed;
    public int dashCount;
    public float dashDistance;
    public float dashCooldown;
}
