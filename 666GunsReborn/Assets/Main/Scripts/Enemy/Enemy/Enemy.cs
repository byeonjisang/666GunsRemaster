using System.Collections.Generic;
using Google.GData.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        #region Parameters and Components
        // 상태들을 직접 생성하고 소유하므로, public로 열어 다른 상태가 접근하게 함
        public ChaseState ChaseState { get; private set; }
        public AttackState AttackState { get; private set; }
        public DeadState DeadState { get; private set; }

        // 적의 스탯 데이터
        [Header("Enemy Data")]
        [SerializeField] private EnemyData enemyData;
        // 적 무기 장착 위치
        [Header("Weapon")]
        [SerializeField] private Transform weaponSocket;
        // 총구 위치들
        [SerializeField] private List<Transform> embeddedMuzzles;

        // 총구 위치들
        public List<Transform> ActiveMuzzle { get; private set; } = new List<Transform>();

        // 적의 스탯 컴포넌트
        public EnemyStat EnemyStat { get; private set; }
        // 플레이어 추적용 변수
        public Transform PlayerTransform { get; private set; }
        // 적 AI 컴포넌트
        public NavMeshAgent NavMeshAgent { get; private set; }
        // 적 애니메이터 컴포넌트
        public Animator Animator { get; private set; }

        // 공격 중인지 여부
        public bool IsAttacking { get; set; } = false;
        // 사망 여부
        private bool isDead = false;

        // 상태 머신 컨텍스트
        private EnemyStateContext _stateContext;
        #endregion

        #region Awake
        private void Awake()
        {
            // 필요한 컴포넌트 초기화
            NavMeshAgent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            PlayerTransform = GameObject.FindWithTag("Player").transform;

            // 무기 장착
            SetupWeaponAndMuzzle();

            // 상태 머신 컨텍스트 생성
            _stateContext = new EnemyStateContext(this);

            // 스탯 초기화
            EnemyStat = new EnemyStat(enemyData);
            NavMeshAgent.speed = EnemyStat.MoveSpeed;

            // 상태 객체들 생성
            ChaseState = new ChaseState(this, _stateContext);
            AttackState = new AttackState(this, _stateContext);
            DeadState = new DeadState(this, this, _stateContext);

            // 초기 상태 설정
            _stateContext.Transition(ChaseState);
        }
        #endregion

        #region Weapon Setup
        // 무기 장착 및 총구 찾는 메서드
        private void SetupWeaponAndMuzzle()
        {
            // 총기를 소지한 애
            if (enemyData.weaponPrefab != null && weaponSocket != null)
            {
                GameObject weapon = Instantiate(enemyData.weaponPrefab, transform);
                weapon.transform.SetParent(weaponSocket);
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localRotation = Quaternion.identity;

                // 총구 위치들 찾기
                ActiveMuzzle.Add(weapon.transform.Find("Muzzle"));
            }
            // 총기가 아닌 몸에 총구가 있을 때
            else if (embeddedMuzzles.Count > 0)
            {
                ActiveMuzzle = embeddedMuzzles;
            }
            // 둘 다 없을 때
            else
            {
                ActiveMuzzle.Add(weaponSocket != null ? weaponSocket : transform);
            }
        }
        #endregion

        #region Update
        private void Update()
        {
            // 매 프레임마다 현재 상태의 UpdateState 메서드 호출
            if (_stateContext.CurrentState != null)
                _stateContext.CurrentState.UpdateState();
        }
        #endregion

        #region Enemy Attack
        /// <summary>
        /// 플레이어가 공격 범위 안에 있는지 확인하는 메서드
        /// </summary>
        /// <returns></returns>
        public bool IsPlayerInAttackRange()
        {
            // 공격 범위 안에 있는가 체크하기 위해 거리 측정
            float distance = Vector3.Distance(transform.position, PlayerTransform.position);

            // 공격 범위 안에 있으면서 정면에 있는지 확인
            Vector3 directionToPlayer = (PlayerTransform.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);

            return distance <= EnemyStat.AtkRange && dotProduct > 0.8f;
        }

        /// <summary>
        /// 공격 애니메이션이 끝났을 때 호출되는 메서드
        /// </summary>
        public void CheckedAttackAnimationEnd()
        {
            IsAttacking = false;
        }
        #endregion

        #region Hit and Die
        /// <summary>
        /// 적이 공격을 받았을 때 호출되는 메서드
        /// </summary>
        public void TakeDamage(int damage)
        {
            // 이미 죽은 적이면 무시
            if (isDead)
                return;
                
            // 적이 살아있으면 true 죽으면 false 반환
            if (!EnemyStat.TakeDamage(damage))
                Die();
        }

        // 적이 죽었을 때 호출되는 메서드
        private void Die()
        {
            Debug.Log("Enemy died");
            isDead = true;
            _stateContext.Transition(DeadState);
        }
        #endregion
    }
}