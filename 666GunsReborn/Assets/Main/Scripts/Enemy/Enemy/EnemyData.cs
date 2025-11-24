using Enemy;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Attack Type")]
    public Enemy.AttackType attackType;
    public AttackStrategy attackStrategy;
    public GameObject weaponPrefab;

    [Header("Enemy Stat")]
    public int atk;
    public float atkSpeed;
    public float atkRange;
    public float maxHp;
    public float moveSpeed;
}
