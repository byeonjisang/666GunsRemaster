using UnityEngine;

namespace Character.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Datas/EnemyData", order = 1)]
    public class EnemyData : CharacterData
    {
        [Header("Enemy Attack Type")]
        // 공격 타입
        [SerializeField] private AttackType attackType;
        public AttackType AttackType => attackType;
        // 공격 전략
        [SerializeField] private AttackStrategy attackStrategy;
        public AttackStrategy AttackStrategy => attackStrategy;
        // 무기 프리팹
        [SerializeField] private GameObject weaponPrefab;
        public GameObject WeaponPrefab => weaponPrefab;

        [Header("Enemy Stat")]
        // 적 공격력
        [SerializeField] private int attackPower;
        public int AttackPower => attackPower;
        // 적 공격 속도
        [SerializeField] private int attackSpeed;
        public int AttackSpeed => attackSpeed;
        // 적 공격 범위
        [SerializeField] private int attackRange;
        public int AttackRange => attackRange;
    }   
}