using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Police1", menuName = "Police1", order = int.MaxValue)]
public class Police1 : ScriptableObject
{
    //근거리 경찰 몬스터의 기본 정보
    [SerializeField]
    private string enemyName;
    public string GetMonsterName { get { return enemyName; } }
    [SerializeField]
    private float hp = 120f;
    public float GetHp() { return hp; }
    public void SetHp(float damage) { hp = damage; }

    [SerializeField]
    private float currentHp = 10f;
    public float GetCurrentHp() { return currentHp; }

    [SerializeField]
    private float damage = 2f;
    public float GetDamage() { return damage; }

    [SerializeField]
    private float sightRange = 5f;
    public float GetSightRange { get { return sightRange; } }

    [SerializeField]
    private float moveSpeed = 3f;
    public float GetMoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    private float attackRange = 2f;
    public float GetAttackRange { get { return attackRange; } }

    public Police1 Clone()
    {
        Police1 clone = ScriptableObject.CreateInstance<Police1>();
        clone.hp = this.hp;
        clone.moveSpeed = this.moveSpeed;
        return clone;
    }
}
