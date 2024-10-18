using Character.Player.State;
using System.Collections;
using UnityEngine;

namespace Character.Player
{
    public class PlayerDashState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;

        private Rigidbody2D rigid;

        private float dashTimeLeft;
        private bool isDashing = false;

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

            dashTimeLeft = _playerController.DashDuration;
            isDashing = true;
        }

        private void FixedUpdate()
        {
            if (isDashing)
            {
                Vector2 tmpDir = new Vector2(_playerController.Joystick.Horizontal, _playerController.Joystick.Vertical);

                Debug.Log(_playerController.DashSpeed);
                rigid.velocity = tmpDir.normalized * _playerController.DashSpeed;
                dashTimeLeft -= Time.deltaTime;

                if(dashTimeLeft <= 0)
                {
                    isDashing = false;
                    rigid.velocity = Vector2.zero;
                }
            }
        }
    }
}