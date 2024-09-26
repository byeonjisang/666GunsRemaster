using Character.Player.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        //�ܺ� ����
        [SerializeField]
        private PlayerData playerData;
        public FloatingJoystick Joystick;
        [SerializeField]
        private Button dashButton;

        //�÷��̾� ������
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
        private float _cooldownTimeLeft;    //���� ��Ÿ�� �ð�
        private bool _isCooldown = false;   //��Ÿ�� ����

        //�÷��̾� ������Ʈ
        private Rigidbody2D rigid;
        private Animator anim;
        private SpriteRenderer sprite;

        //���� ����
        private IPlayerState _moveState, _stopState, _attackState, _dashState;
        private PlayerStateContext _playerStateContext;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();

            StateInit();    //������ �ʱ�ȭ
        }

        private void Start()
        {
            //���� ���� �ʱ�ȭ
            _playerStateContext = new PlayerStateContext(this);

            _moveState = gameObject.AddComponent<PlayerMoveState>();
            _stopState = gameObject.AddComponent<PlayerStopState>();
            _dashState = gameObject.AddComponent<PlayerDashState>();

            _playerStateContext.Transition(_stopState);

            //�뽬 ��ư �̺�Ʈ ���
            dashButton.onClick.AddListener(StartDash);
        }

        //������ �ʱ�ȭ
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
            //�뽬 ��Ÿ�� ���
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
            //�̵� �� ���� ���� �Ǵ�
            if((Joystick.Horizontal != 0 && Joystick.Vertical != 0))
            {
                Debug.Log("�̵�");
                _playerStateContext.Transition(_moveState);
            }
            else
            {
                Debug.Log("����");
                _playerStateContext.Transition(_stopState);
            }


            //�뽬 �׽�Ʈ �ڵ�
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartDash();
            }
        }

        //�뽬 ����
        private void StartDash()
        {
            //�뽬�� ���� ��� ���� ����
            if (_isCooldown)
                return;

            Debug.Log("�뽬");
            _playerStateContext.Transition(_dashState);

            _cooldownTimeLeft = _dashCooldown;
            _isCooldown = true;
        }
    }
}