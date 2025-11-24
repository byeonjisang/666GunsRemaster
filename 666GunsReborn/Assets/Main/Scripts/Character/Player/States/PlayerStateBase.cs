using UnityEngine;

namespace Character.Player
{
    // 상태 패턴 베이스
    public abstract class PlayerStateBase
    {
        protected Player player;

        public PlayerStateBase(Player player)
        {
            this.player = player;
        }

        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void OnDash() { }
        public abstract void HandleInput(Vector3 direction);
        public virtual void Update() { }
    }    
}
