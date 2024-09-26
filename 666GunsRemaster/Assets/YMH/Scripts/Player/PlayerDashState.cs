using Character.Player.State;
using System.Collections;
using UnityEngine;

namespace Character.Player
{
    public class PlayerDashState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;

        public void Movement(PlayerController playerController)
        {
            if (_playerController == null)
            {
                _playerController = playerController;
            }
        }
    }
}