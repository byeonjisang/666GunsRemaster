using Character.Player.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Player 
{
    public class PlayerMoveState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;

        private Rigidbody2D rigid;
        private Animator anim;
        private SpriteRenderer sprite;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }

        public void Movement(PlayerController playerController)
        {
            if (!_playerController)
            {
                _playerController = playerController;
            }

            _playerController.CurrentSpeed = _playerController.MoveSpeed;
        }
        public void HandleMovement()
        {
            float joyStickInputX = _playerController.Joystick.Horizontal;
            float joyStickInputY = _playerController.Joystick.Vertical;

            float keyboardInputX = Input.GetAxis("Horizontal");
            float keyboardInputY = Input.GetAxis("Vertical");

            float finalInputX = joyStickInputX + keyboardInputX;
            float finalInputY = joyStickInputY + keyboardInputY;

            rigid.velocity = new Vector2(finalInputX * _playerController.MoveSpeed, finalInputY * _playerController.MoveSpeed);

            anim.SetFloat("Speed", rigid.velocity.magnitude);

            if (finalInputX != 0)
                sprite.flipX = finalInputX > 0;
        }

        public void FixedUpdate()
        {
            if (_playerController)
            {
                if(_playerController.CurrentSpeed > 0)
                {
                    HandleMovement();
                }
            }
        }
    }
}