using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement _playerMovement;

        [SerializeField]
        private PlayerData playerData;
        [SerializeField]
        private FloatingJoystick joystick;

        private int _health;
        private float _moveSpeed;
        private float _dashSpeed;
        private float _dashDuration;
        private float _dashCooldown;
        private bool _isDashing = false;
        private float _dashTimeLeft;
        private float _cooldownTimeLeft;
        private bool _isCooldown = false;

        private Rigidbody2D rigid;
        private Animator anim;
        private SpriteRenderer sprite;

        private void Awake()
        {
            //playerMovement = gameObject.AddComponent(PlayerMovement_YMH) as PlayerMovement_YMH;

            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();

            StateInie();
        }
        private void StateInie()
        {
            _health = playerData.health;
            _moveSpeed = playerData.moveSpeed;
            _dashSpeed = playerData.dashSpeed;
            _dashDuration = playerData.dashDuration;
            _dashCooldown = playerData.dashCooldown;
        }

        private void FixedUpdate()
        {
            //HandleMovement();
        }
    }

}