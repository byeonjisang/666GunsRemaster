using System.Xml.Serialization;
using Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class ChaseState : IState
    {
        // Enemy로부터 필요한 컴포넌트를 담을 변수들
        private Enemy _enemy;
        private EnemyStateContext _stateContext;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private Transform _target;

        // 생성자: Enemy와 상태 컨텍스트를 받아 초기화
        public ChaseState(Enemy enemy, EnemyStateContext stateContext)
        {
            _enemy = enemy;
            _stateContext = stateContext;
            _animator = _enemy.Animator;
            _navMeshAgent = _enemy.NavMeshAgent;
            _target = _enemy.PlayerTransform;
        }

        // 추격 상태에 들어갈 때 초기 설정
        public void EnterState()
        {
            // 추격 상태에 들어갈 때 초기 설정
            _navMeshAgent.isStopped = false; // NavMeshAgent 활성화
            //_animator.SetBool("isWalk", true); // 걷기 애니메이션 시작
        }

        // 매 프레임마다 호출되어 플레이어를 추격하고 상태 전환을 관리
        public void UpdateState()
        {
            // 플레이어 추격
            _navMeshAgent.SetDestination(_target.position);

            // 플레이어가 공격 범위 안에 있는 경우 공격 상태로 변환
            if (_enemy.IsPlayerInAttackRange())
            {
                _stateContext.Transition(_enemy.AttackState);
            }
        }

        // 추격 상태에서 나올 때 설정
        public void ExitState()
        {
            _animator.SetBool("isWalk", false);
        }
    }
}