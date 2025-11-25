using UnityEngine;

namespace Character.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Datas/EnemyData", order = 1)]
    public class EnemyData : CharacterData
    {
        [Header("Enemy Attack Type")]
        public AttackType attackType;
        public AttackStrategy attackStrategy;
        public GameObject weaponPrefab;

        [Header("Enemy Stat")]
        public int attackPower;
        public int attackSpeed;
        public int attackRange;
    }   
}