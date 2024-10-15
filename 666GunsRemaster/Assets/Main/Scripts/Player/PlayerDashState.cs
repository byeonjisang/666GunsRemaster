using Character.Player.State;
using System.Collections;
using UnityEngine;

namespace Character.Player
{
    public class PlayerDashState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;

        private Rigidbody2D rigid;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public void Movement(PlayerController playerController)
        {
            if (_playerController == null)
            {
                _playerController = playerController;
            }

            StartDash();
        }

        private void StartDash()
        {
            Vector2 tmpDir = new Vector2(_playerController.Joystick.Horizontal, _playerController.Joystick.Vertical);
            
            float dashTimeLeft = _playerController.DashDuration;

            while(dashTimeLeft >= 0)
            {
                //rigid.velocity = new Vector2(joyStickInputX * _playerController.DashSpeed, joyStickInputY * _playerController.DashSpeed);
                rigid.velocity = tmpDir.normalized * _playerController.DashSpeed;
                dashTimeLeft -= Time.deltaTime;
            }
        }
    }
}