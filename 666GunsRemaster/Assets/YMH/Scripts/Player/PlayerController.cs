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

        private int _health;                //체력
        private float _moveSpeed;           //이동속도
        private float _dashSpeed;           //대쉬속도
        private float _dashDuration;        //대쉬지속시간
        private float _dashCooldown;        //대쉬쿨타임
        private bool _isDashing = false;    //대쉬 여부
        private float _dashTimeLeft;        //남은 대쉬 시간
        private float _cooldownTimeLeft;    //남은 쿨타임 시간
        private bool _isCooldown = false;   //쿨타임 여부

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

        //데이터 초기화
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