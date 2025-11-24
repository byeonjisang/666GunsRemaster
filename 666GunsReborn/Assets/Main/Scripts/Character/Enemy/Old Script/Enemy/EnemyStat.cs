<<<<<<<< HEAD:666GunsReborn/Assets/Main/Scripts/Character/Enemy/Old Script/Enemy/EnemyStat.cs
using UnityEngine;

namespace Enemy
{
    public class EnemyStat : MonoBehaviour
    {
        // 공격 타입
        private AttackType _attackType;
        // 공격 전략
        private IAttackStrategy _attackStrategy;

        // 공격력
        private int _atk;
        // 공격 속도
        private float _atkSpeed;
        // 공격 범위
        private float _atkRange;
        // 최대 체력
        private float _maxHp;
        // 현재 체력
        private float _currentHp;
        // 이동 속도
        private float _moveSpeed;

        // 외부 접근용 프로퍼티
        public AttackType AttackType => _attackType;
        public IAttackStrategy AttackStrategy => _attackStrategy;
        public int Atk => _atk;
        public float AtkSpeed => _atkSpeed;
        public float AtkRange => _atkRange;
        public float CurrentHp => _currentHp;
        public float MoveSpeed => _moveSpeed;

        // 생성자
        public EnemyStat(EnemyData enemyData)
        {
            Init(enemyData);
        }

        /// <summary>
        /// 적 스탯 초기화 메서드
        /// </summary>
        /// <param name="enemyData"></param>
        public void Init(EnemyData enemyData)
        {
            _attackType = enemyData.attackType;
            _attackStrategy = enemyData.attackStrategy;
            _atk = enemyData.atk;
            _atkSpeed = enemyData.atkSpeed;
            _atkRange = enemyData.atkRange;
            _maxHp = enemyData.maxHp;
            _currentHp = _maxHp; // 초기 현재 체력은 최대 체력과 동일
            _moveSpeed = enemyData.moveSpeed;
        }

        /// <summary>
        /// 데미지 처리 메서드
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public bool TakeDamage(int damage)
        {
            // 데미지 부여
            _currentHp -= damage;
            // 체력이 0 이하이면 false 반환
            if (_currentHp <= 0)
                return false;
            return true;
        }
    }
========
using UnityEngine;

namespace Character.Enemy
{
    public class EnemyStat
    {
        // 공격 타입
        private AttackType _attackType;
        // 공격 전략
        private IAttackStrategy _attackStrategy;

        // 공격력
        private int _attack;
        // 공격 속도
        private float _attackSpeed;
        // 공격 범위
        private float _attackRange;
        // 최대 체력
        private float _maxHealth;
        // 현재 체력
        private float _currentHealth;
        // 이동 속도
        private float _moveSpeed;

        // 외부 접근용 프로퍼티
        public AttackType AttackType => _attackType;
        public IAttackStrategy AttackStrategy => _attackStrategy;
        public int Attack => _attack;
        public float AttackSpeed => _attackSpeed;
        public float AttackRange => _attackRange;
        public float CurrentHealth => _currentHealth;
        public float MoveSpeed => _moveSpeed;

        // 생성자
        public EnemyStat(EnemyData enemyData)
        {
            Init(enemyData);
        }

        /// <summary>
        /// 적 스탯 초기화 메서드
        /// </summary>
        /// <param name="enemyData"></param>
        public void Init(EnemyData enemyData)
        {
            _attackType = enemyData.attackType;
            _attackStrategy = enemyData.attackStrategy;
            _attack = enemyData.attackPower;
            _attackSpeed = enemyData.attackSpeed;
            _attackRange = enemyData.attackRange;
            _maxHealth = enemyData.health;
            _currentHealth = _maxHealth; // 초기 현재 체력은 최대 체력과 동일
            _moveSpeed = enemyData.moveSpeed;
        }

        /// <summary>
        /// 데미지 처리 메서드
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public bool TakeDamage(int damage)
        {
            // 데미지 부여
            _currentHealth -= damage;
            // 체력이 0 이하이면 false 반환
            if (_currentHealth <= 0)
                return false;
            return true;
        }
    }
>>>>>>>> origin/main:666GunsReborn/Assets/Main/Scripts/Character/Enemy/EnemyStat.cs
}