using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public Enemy.AttackType attackType;
    public int atk;
    public float atkSpeed;
    public float atkRange;
    public float maxHp;
    public float moveSpeed;
}
