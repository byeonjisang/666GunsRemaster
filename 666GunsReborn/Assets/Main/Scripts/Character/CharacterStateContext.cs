namespace Character
{
    public class CharacterStateContext
    {
        // 현재 상태를 나타내는 변수
        public IState CurrentState { get; private set; }

        /// <summary>
        /// 상태 전환 메서드
        /// </summary>
        /// <param name="state"></param>
        public void TransitionTo(IState nextState)
        {
            // 현재 상태가 null이 아닐 경우, 현재 상태의 ExitState 메서드 호출
            if(CurrentState != null)
                CurrentState.ExitState();

            // 새로운 상태로 전환하고, 해당 상태의 EnetyState 메서드 호출
            CurrentState = nextState;
            CurrentState.EnterState();
        }

        public void Update()
        {
            if(CurrentState != null)
                CurrentState.UpdateState();
        }
    }    
}

