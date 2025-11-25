using UnityEngine;
using UnityEngine.AI;

namespace Character.Enemy
{
    public class AttackState : IState
    {
        // Enemy로부터 필요한 컴포넌트를 담을 변수들
        private Enemy _enemy;

        private float _lastAttackTime;

        // 생성자: Enemy와 상태 컨텍스트를 받아 초기화
        public AttackState(Enemy enemy)
        {
            _enemy = enemy;
        }

        // 추격 상태에 들어갈 때 초기 설정
        public void EnterState()
        {
            // 사망 상태에 들어갈 때 초기 설정
            _enemy.NavMeshAgent.isStopped = true;
            
            // 공격 시간 초기화
            _lastAttackTime = 0f;
        }

        // 공격 처리
        public void UpdateState()
        {
            if (Time.time >= _lastAttackTime + _enemy.EnemyStat.AttackSpeed)
            {
                _lastAttackTime = Time.time;
                _enemy.Anim.SetTrigger("Attack");
                _enemy.EnemyStat.AttackStrategy?.Execute(_enemy);
                _enemy.IsAttacking = true;
                Debug.Log("공격!");
            }

            // 플레이어가 공격 범위 밖으로 나가면 추격 상태로 변환
            if (!_enemy.IsPlayerInAttackRange())
            {
                // 공격 애니메이션이 끝난 후에만 상태 전환
                if(!_enemy.IsAttacking)
                    _enemy.StateContext.TransitionTo(_enemy.StateContext.ChaseState); 
            }
        }

        // 공격 상태에서 나올 때 설정
        public void ExitState()
        {
            
        }
    }
}