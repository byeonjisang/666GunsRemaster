using System.Collections;
using UnityEngine;

namespace Character.Player.State
{
    public class PlayerStateContext
    {
        public IPlayerState CurrentState{ get; set; }

        private readonly PlayerController _playerController;

        public PlayerStateContext(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void Transition()
        {
            CurrentState.Movement(_playerController);
        }
        public void Transition(IPlayerState state)
        {
            CurrentState = state;
            CurrentState.Movement(_playerController);
        }
    }
}