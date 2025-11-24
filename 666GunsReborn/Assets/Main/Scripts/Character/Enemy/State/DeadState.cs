<<<<<<< HEAD
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Character.Enemy
{
    public class DeadState : IState
    {
        // Enemy로부터 필요한 컴포넌트를 담을 변수들
        private Enemy _enemy;

        // 사망 후 대기 시간
        private float _deadDuration = 3f;

        // 생성자: Enemy와 상태 컨텍스트를 받아 초기화
        public DeadState(Enemy enemy)
        {
            _enemy = enemy;
        }

        // 사망 후 사라지는 과정 시작
        private void StartDisappearSequence()
        {
            //StopAllCoroutines();
            _enemy.StartCoroutine(DisappearCoroutine());
            
        }
        
        // 디졸드 효과 코루틴
        private IEnumerator DisappearCoroutine()
        {
            yield return new WaitForSeconds(_deadDuration); // 사망 애니메이션 재생 대기

            float duration = 1f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float splitValue = Mathf.Lerp(2f, 0f, elapsed / duration);
                _enemy.Material.SetFloat(_enemy.SplitValueID, splitValue);
                yield return null;
            }

            // 오브젝트 제거
            GameObject.Destroy(_enemy.gameObject);
        }

        // 사망 상태에 들어갈 때 초기 설정
        public void EnterState()
        {
            // 사망 상태에 들어갈 때 초기 설정
            _enemy.NavMeshAgent.isStopped = true; // NavMeshAgent 비활성화
            _enemy.Anim.SetTrigger("Dead"); // 사망 애니메이션 시작

            StartDisappearSequence();
        }

        // 사망 후 처리
        public void UpdateState()
        {
            
        }

        // 사망 상태에서 나올 때 설정
        public void ExitState()
        {
            //_animator.SetBool("isDead", false);
        }
    }
=======
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Character.Enemy
{
    public class DeadState : IState
    {
        // Enemy로부터 필요한 컴포넌트를 담을 변수들
        private Enemy _enemy;

        // 사망 후 대기 시간
        private float _deadDuration = 3f;

        // 생성자: Enemy와 상태 컨텍스트를 받아 초기화
        public DeadState(Enemy enemy)
        {
            _enemy = enemy;
        }

        // 사망 후 사라지는 과정 시작
        private void StartDisappearSequence()
        {
            //StopAllCoroutines();
            _enemy.StartCoroutine(DisappearCoroutine());
            
        }
        
        // 디졸드 효과 코루틴
        private IEnumerator DisappearCoroutine()
        {
            yield return new WaitForSeconds(_deadDuration); // 사망 애니메이션 재생 대기

            float duration = 1f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float splitValue = Mathf.Lerp(2f, 0f, elapsed / duration);
                _enemy.Material.SetFloat(_enemy.SplitValueID, splitValue);
                yield return null;
            }

            // 오브젝트 제거
            GameObject.Destroy(_enemy.gameObject);
        }

        // 사망 상태에 들어갈 때 초기 설정
        public void EnterState()
        {
            // 사망 상태에 들어갈 때 초기 설정
            _enemy.NavMeshAgent.isStopped = true; // NavMeshAgent 비활성화
            _enemy.Anim.SetTrigger("Dead"); // 사망 애니메이션 시작

            StartDisappearSequence();
        }

        // 사망 후 처리
        public void UpdateState()
        {
            
        }

        // 사망 상태에서 나올 때 설정
        public void ExitState()
        {
            //_animator.SetBool("isDead", false);
        }
    }
>>>>>>> origin/main
}