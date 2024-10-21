using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Police1", menuName = "Police1", order = int.MaxValue)]
public class Police1 : ScriptableObject
{
    //������ �⺻ ����
    [SerializeField]
    private string enemyName;
    public string GetMonsterName { get { return enemyName; } }
    [SerializeField]
    private float hp = 10f;
    public float GetHp() { return hp; }
    public void SetHp(float damage) { hp = damage; }

    [SerializeField]
    private float currentHp = 10f;
    public float GetCurrentHp() { return currentHp; }

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
