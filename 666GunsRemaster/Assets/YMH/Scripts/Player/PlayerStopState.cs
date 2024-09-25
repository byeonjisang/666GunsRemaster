using Character.Player;
using Character.Player.State;
using System.Collections;
using UnityEngine;

namespace Character.Player
{
    public class PlayerStopState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;

        public void Movement(PlayerController playerController)
        {
            if (!_playerController)
            {
                _playerController = playerController;
            }

            playerController.CurrentSpeed = 0;
        }
    }
}