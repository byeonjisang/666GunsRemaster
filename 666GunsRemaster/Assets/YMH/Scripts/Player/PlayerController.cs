using Character.Player.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        //private PlayerMovement _playerMovement;

        [SerializeField]
        private PlayerData playerData;
        [SerializeField]
        private FloatingJoystick joystick;

        public int Health { get { return _health; } }
        public float CurrentSpeed { get; set; }
        public float MoveSpeed { get { return _moveSpeed; } }
        public float DashSpeed { get { return _dashSpeed; } }
        public float DashDuration { get { return _dashDuration; } }
        public float DashCooldown { get { return _dashCooldown; } }

        private int _health;                //ü��
        private float _moveSpeed;           //�̵��ӵ�
        private float _dashSpeed;           //�뽬�ӵ�
        private float _dashDuration;        //�뽬���ӽð�
        private float _dashCooldown;        //�뽬��Ÿ��
        private bool _isDashing = false;    //�뽬 ����
        private float _dashTimeLeft;        //���� �뽬 �ð�
        private float _cooldownTimeLeft;    //���� ��Ÿ�� �ð�
        private bool _isCooldown = false;   //��Ÿ�� ����

        private Rigidbody2D rigid;
        private Animator anim;
        private SpriteRenderer sprite;

        private IPlayerState _moveState, _stopState, _attackState, _dashState;
        private PlayerStateContext _playerStateContext;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();

            StateInie();
        }

        private void Start()
        {
            _playerStateContext = new PlayerStateContext(this);

            _moveState = gameObject.AddComponent<PlayerMoveState>();
            _stopState = gameObject.AddComponent<PlayerStopState>();

            _playerStateContext.Transition(_stopState);
        }

        //������ �ʱ�ȭ
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
            if(joystick.Horizontal != 0 && joystick.Vertical != 0)
            {
                _playerStateContext.Transition(_moveState);
            }
            else
            {
                _playerStateContext.Transition(_stopState);
            }
        }
    }
}