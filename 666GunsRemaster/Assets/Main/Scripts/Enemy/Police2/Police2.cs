using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Police2", menuName = "Police2", order = int.MaxValue)]
public class Police2 : ScriptableObject
{
    //몬스터의 기본 정보
    [SerializeField]
    private string enemyName;
    public string GetMonsterName { get { return enemyName; } }
    [SerializeField]
    private int hp;
    public int GetHp { get { return hp; } }
    [SerializeField]
    private int damage;
    public int GetDamage { get { return damage; } }
    [SerializeField]
    private float sightRange;
    public float GetSightRange { get { return sightRange; } }
    [SerializeField]
    private float moveSpeed;
    public float GetMoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    private float attackRange;
    public float GetAttackRange { get { return attackRange; } }
}