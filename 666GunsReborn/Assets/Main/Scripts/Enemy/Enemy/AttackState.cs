using System.Xml.Serialization;
using Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AttackState : IState
    {
        // Enemy로부터 필요한 컴포넌트를 담을 변수들
        private Enemy _enemy;
        private EnemyStateContext _stateContext;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private IAttackStrategy _attackStrategy;

        private float _lastAttackTime;

        // 생성자: Enemy와 상태 컨텍스트를 받아 초기화
        public AttackState(Enemy enemy, EnemyStateContext stateContext)
        {
            _enemy = enemy;
            _stateContext = stateContext;
            _animator = _enemy.Animator;
            _navMeshAgent = _enemy.NavMeshAgent;
            _attackStrategy = enemy.EnemyStat.AttackStrategy;
        }

        // 추격 상태에 들어갈 때 초기 설정
        public void EnterState()
        {
            // 사망 상태에 들어갈 때 초기 설정
            _navMeshAgent.isStopped = true;
            
            // 공격 시간 초기화
            _lastAttackTime = 0f;
        }

        // 공격 처리
        public void UpdateState()
        {
            if (Time.time >= _lastAttackTime + _enemy.EnemyStat.AtkSpeed)
            {
                _lastAttackTime = Time.time;
                _animator.SetTrigger("Attack");
                _attackStrategy?.Execute(_enemy);
                _enemy.IsAttacking = true;
                Debug.Log("공격!");
            }

            // 플레이어가 공격 범위 밖으로 나가면 추격 상태로 변환
            if (!_enemy.IsPlayerInAttackRange())
            {
                // 공격 애니메이션이 끝난 후에만 상태 전환
                if(!_enemy.IsAttacking)
                    _stateContext.Transition(_enemy.ChaseState); 
            }
        }

        // 공격 상태에서 나올 때 설정
        public void ExitState()
        {
            
        }
    }
}