using Character.Player.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        //외부 참조
        [SerializeField]
        private PlayerData playerData;
        public FloatingJoystick Joystick;
        [SerializeField]
        private Button dashButton;

        //플레이어 데이터
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
        private float _cooldownTimeLeft;    //남은 쿨타임 시간
        private bool _isCooldown = false;   //쿨타임 여부

        //플레이어 컴포넌트
        private Rigidbody2D rigid;
        private Animator anim;
        private SpriteRenderer sprite;

        //상태 패턴
        private IPlayerState _moveState, _stopState, _attackState, _dashState;
        private PlayerStateContext _playerStateContext;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();

            StateInit();    //데이터 초기화
        }

        private void Start()
        {
            //상태 패턴 초기화
            _playerStateContext = new PlayerStateContext(this);

            _moveState = gameObject.AddComponent<PlayerMoveState>();
            _stopState = gameObject.AddComponent<PlayerStopState>();
            _dashState = gameObject.AddComponent<PlayerDashState>();

            _playerStateContext.Transition(_stopState);

            //대쉬 버튼 이벤트 등록
            dashButton.onClick.AddListener(StartDash);
        }

        //데이터 초기화
        private void StateInit()
        {
            _health = playerData.health;
            _moveSpeed = playerData.moveSpeed;
            _dashSpeed = playerData.dashSpeed;
            _dashDuration = playerData.dashDuration;
            _dashCooldown = playerData.dashCooldown;
        }

        private void Update()
        {
            //대쉬 쿨타임 계산
            if (_isCooldown)
            {
                _cooldownTimeLeft -= Time.deltaTime;
                if(_cooldownTimeLeft <= 0)
                {
                    _isCooldown = false;
                }
            }
        }

        private void FixedUpdate()
        {
            //이동 및 정지 상태 판단
            if((Joystick.Horizontal != 0 && Joystick.Vertical != 0))
            {
                Debug.Log("이동");
                _playerStateContext.Transition(_moveState);
            }
            else
            {
                Debug.Log("정지");
                _playerStateContext.Transition(_stopState);
            }


            //대쉬 테스트 코드
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartDash();
            }
        }

        //대쉬 시작
        private void StartDash()
        {
            //대쉬가 쿨일 경우 실행 안함
            if (_isCooldown)
                return;

            Debug.Log("대쉬");
            _playerStateContext.Transition(_dashState);

            _cooldownTimeLeft = _dashCooldown;
            _isCooldown = true;
        }
    }
}