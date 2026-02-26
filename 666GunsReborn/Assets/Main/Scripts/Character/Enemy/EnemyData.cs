using UnityEngine;

namespace Character.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Datas/EnemyData", order = 1)]
    public class EnemyData : CharacterData
    {   
        // 적을 소환할 때 필요한 정보들
        // [Header("Enemy Spawn Info")]
        // // 적 Mesh
        // [SerializeField] private string meshKey;
        // public string MeshKey => meshKey;
        // // 적 머테리얼
        // [SerializeField] private string materialKey;
        // public string MaterialKey => materialKey;
        // // 적 뼈대
        // [SerializeField] private string prefabKey;
        // public string PrefabKey => prefabKey;
        // // 적 애니메이션 키
        // [SerializeField] private string animationKey;
        // public string AnimationKey => animationKey;

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