using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Police1", menuName = "Police1", order = int.MaxValue)]
public class Police1 : ScriptableObject
{
    //몬스터의 기본 정보
    [SerializeField]
    private string enemyName;
    public string GetMonsterName { get { return enemyName; } }
    [SerializeField]
    private float hp;
    public float GetHp { get { return hp; } }
    public void SetHp(float damage) { hp -= damage; }

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

    public Police1 Clone()
    {
        Police1 clone = ScriptableObject.CreateInstance<Police1>();
        clone.hp = this.hp;
        clone.moveSpeed = this.moveSpeed;
        return clone;
    }
}
