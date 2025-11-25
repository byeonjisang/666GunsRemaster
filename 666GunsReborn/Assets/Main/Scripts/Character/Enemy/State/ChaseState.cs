using UnityEngine;
using UnityEngine.AI;

namespace Character.Enemy
{
    public class ChaseState : IState
    {
        // Enemy로부터 필요한 컴포넌트를 담을 변수들
        private Enemy _enemy;

        // 생성자: Enemy와 상태 컨텍스트를 받아 초기화
        public ChaseState(Enemy enemy)
        {
            _enemy = enemy;
        }

        // 추격 상태에 들어갈 때 초기 설정
        public void EnterState()
        {
            // 추격 상태에 들어갈 때 초기 설정
            _enemy.NavMeshAgent.isStopped = false; // NavMeshAgent 활성화

            // TODO: 걷기 애니메이션 전에 플레이어 쪽으로 회전
            _enemy.Anim.SetBool("IsWalk", true); // 걷기 애니메이션 시작
        }

        // 매 프레임마다 호출되어 플레이어를 추격하고 상태 전환을 관리
        public void UpdateState()
        {
            // 플레이어 추격
            _enemy.NavMeshAgent.SetDestination(_enemy.PlayerTransform.position);

            // 플레이어가 공격 범위 안에 있는 경우 공격 상태로 변환
            if (_enemy.IsPlayerInAttackRange())
            {
                Debug.Log("[ChaseState] 플레이어 공격 범위 진입, 공격 상태로 전환");
                _enemy.StateContext.TransitionTo(_enemy.StateContext.AttackState);
            }
        }

        // 추격 상태에서 나올 때 설정
        public void ExitState()
        {
            _enemy.Anim.SetBool("IsWalk", false);
        }
    }
}