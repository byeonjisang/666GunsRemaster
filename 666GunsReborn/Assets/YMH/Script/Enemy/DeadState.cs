using System.Collections;
using System.Xml.Serialization;
using Enemy;
using Unity.VisualScripting;
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
        private Material _material;
        private MonoBehaviour _runner;

        // 사망 후 대기 시간
        private float _deadDuration = 3f;

        private string _splitValue = "_Split_Value";
        private int _splitValueID;

        // 생성자: Enemy와 상태 컨텍스트를 받아 초기화
        public DeadState(Enemy enemy, MonoBehaviour runner, EnemyStateContext stateContext)
        {
            _enemy = enemy;
            _stateContext = stateContext;
            _animator = _enemy.Animator;
            _navMeshAgent = _enemy.NavMeshAgent;
            _runner = runner;

            _material = _enemy.GetComponent<Renderer>().material;
            // 쉐이더 프로퍼티를 ID로 변환(최적화)
            _splitValueID = Shader.PropertyToID(_splitValue);
        }

        // 사망 후 사라지는 과정 시작
        private void StartDisappearSequence()
        {
            //StopAllCoroutines();
            _runner.StartCoroutine(DisappearCoroutine());
            
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
                Debug.Log("Split Value: " + splitValue);
                _material.SetFloat(_splitValueID, splitValue);
                yield return null;
            }

            // 오브젝트 제거
            GameObject.Destroy(_enemy.gameObject);
        }

        // 사망 상태에 들어갈 때 초기 설정
        public void EnterState()
        {
            // 사망 상태에 들어갈 때 초기 설정
            _navMeshAgent.isStopped = true; // NavMeshAgent 비활성화
            _animator.SetTrigger("Dead"); // 사망 애니메이션 시작

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
}