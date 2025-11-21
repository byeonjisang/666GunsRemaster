using Character;

namespace Character.Player
{
    public class PlayerStateContext : CharacterStateContext
    {
        // 플레이어의 각 상태 인스터스
        public IdleState IdleState;
        public MoveState MoveState;
        public DashState DashState;

        public PlayerStateContext(Player player)
        {
            IdleState = new IdleState(player);
            MoveState = new MoveState(player);
            DashState = new DashState(player);
        }
    }    
}

