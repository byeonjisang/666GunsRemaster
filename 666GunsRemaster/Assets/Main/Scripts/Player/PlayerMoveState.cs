using Character.Player.State;
using Gun;
using UnityEngine;

namespace Character.Player 
{
    public class PlayerMoveState : MonoBehaviour, IPlayerState
    {
        private WeaponManager weaponManager;
        private PlayerController _playerController;

        private Rigidbody2D rigid;
        private Animator anim;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            weaponManager = GetComponentInChildren<WeaponManager>();
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

            rigid.velocity = new Vector2(finalInputX * _playerController.CurrentSpeed, finalInputY * _playerController.CurrentSpeed);

            anim.SetFloat("Speed", rigid.velocity.magnitude);
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