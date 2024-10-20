using Character.Player.State;
using System.Collections;
using UnityEngine;

namespace Character.Player
{
    public class PlayerDashState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;
        private Ghost ghost;

        private Rigidbody2D rigid;
        private Animator anim;

        private float dashTimeLeft;
        private bool isDashing = false;

        private void Awake()
        {
            ghost = GetComponent<Ghost>();

            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        public void Movement(PlayerController playerController)
        {
            if (_playerController == null)
            {
                _playerController = playerController;
            }

            dashTimeLeft = _playerController.DashDuration;
            isDashing = true;
            ghost.makeGhost = true;
        }

        private void FixedUpdate()
        {
            if (isDashing)
            {
                Vector2 tmpDir = new Vector2(_playerController.Joystick.Horizontal, _playerController.Joystick.Vertical);

                rigid.velocity = tmpDir.normalized * _playerController.DashSpeed;
                dashTimeLeft -= Time.deltaTime;

                if(dashTimeLeft <= 0)
                {
                    isDashing = false;
                    rigid.velocity = Vector2.zero;
                    anim.SetBool("IsDash", false);
                    ghost.makeGhost = false;
                }
            }
        }
    }
}