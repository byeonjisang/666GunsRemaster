using System.Xml.Serialization;
using Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class DeadState : IState
    {
        // Enemy로부터 필요한 컴포넌트를 담을 변수들
        private Enemy _enemy;
        private EnemyStateContext _stateContext;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;

        // 생성자: Enemy와 상태 컨텍스트를 받아 초기화
        public DeadState(Enemy enemy, EnemyStateContext stateContext)
        {
            _enemy = enemy;
            _stateContext = stateContext;
            _animator = _enemy.Animator;
            _navMeshAgent = _enemy.NavMeshAgent;
        }

        // 추격 상태에 들어갈 때 초기 설정
        public void EnterState()
        {
            // 사망 상태에 들어갈 때 초기 설정
            _navMeshAgent.isStopped = true; // NavMeshAgent 비활성화
            _animator.SetBool("isDead", true); // 사망 애니메이션 시작
        }

        // 사망 후 처리
        public void UpdateState()
        {
            
        }

        // 사망 상태에서 나올 때 설정
        public void ExitState()
        {
            _animator.SetBool("isDead", false);
        }
    }
}