using UnityEngine;

namespace Character
{
    public interface IState
    {
        // 상태 전환 시 초기 설정
        public void EnterState();
        // 상태가 활성 상태일 때 실행되는 로직
        public void UpdateState();
        // 상태에서 빠져나올 때 필요한 정리 작업 수행
        public void ExitState();
    }
}