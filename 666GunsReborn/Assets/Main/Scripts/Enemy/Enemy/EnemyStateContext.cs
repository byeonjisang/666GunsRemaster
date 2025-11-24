using UnityEngine;

namespace Enemy
{
    public class EnemyStateContext
    {
        // 현재 적 AI가 가지고 있는 상태 저장
        public IState CurrentState;

        private readonly Enemy _controller;

        // 생성자
        public EnemyStateContext(Enemy controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// 상태 전환 메서드
        /// </summary>
        /// <param name="state"></param>
        public void Transition(IState state)
        {
            // 현재 상태가 null이 아닐 경우, 현재 상태의 ExitState 메서드 호출
            if (CurrentState != null)
                CurrentState.ExitState();

            // 새로운 상태로 전환하고, 해당 상태의 EnterState 메서드 호출
            CurrentState = state;
            CurrentState.EnterState();
        }
    }
}