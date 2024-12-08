using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Police2", menuName = "Police2", order = int.MaxValue)]
public class Police2 : ScriptableObject
{
    //���Ÿ� ���� ������ �⺻ ����
    [SerializeField]
    private string enemyName;
    public string GetMonsterName { get { return enemyName; } }
    [SerializeField]
    private float hp = 100f;
    public float GetHp() { return hp; }
    public void SetHp(float damage) { hp = damage; }

    [SerializeField]
    private float currentHp = 10f;
    public float GetCurrentHp() { return currentHp; }

    [SerializeField]
    private int damage = 1;
    public int GetDamage { get { return damage; } }
    [SerializeField]
    private float sightRange = 10f;
    public float GetSightRange { get { return sightRange; } }
    [SerializeField]
    private float moveSpeed = 2f;
    public float GetMoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    private float attackRange = 5f;
    public float GetAttackRange { get { return attackRange; } }

    public Police2 Clone()
    {
        Police2 clone = ScriptableObject.CreateInstance<Police2>();
        clone.hp = this.hp;
        clone.moveSpeed = this.moveSpeed;
        return clone;
    }
}