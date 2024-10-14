using Character.Player.State;
using Gun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

namespace Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        //�ܺ� ����
        [SerializeField]
        private PlayerData playerData;
        private MonsterScannerTest monsterScannerTest;
        private WeaponManager weaponManager;
        public FloatingJoystick Joystick;
        [SerializeField]
        private Button dashButton;
        [SerializeField]
        private Button fireButton;

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

        private bool _isFire = false;       //�� �߻� ����
        private bool _isTarget = false;

        //�÷��̾� ������Ʈ
        private Rigidbody2D rigid;
        private Animator anim;
        private SpriteRenderer sprite;

        //���� ����
        private IPlayerState _moveState, _stopState, _fireState, _dashState;
        private PlayerStateContext _playerStateContext;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();

            weaponManager =GetComponentInChildren<WeaponManager>();
            monsterScannerTest = GetComponent<MonsterScannerTest>();

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
            fireButton.onClick.AddListener(StartFire);
        }

        //������ �ʱ�ȭ
        public void StateInit()
        {
            _health = playerData.maxHealth;
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
                _playerStateContext.Transition(_moveState);
            }
            else
            {
                _playerStateContext.Transition(_stopState);
            }

            RotateGunTowardsTarget();

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
       
        private void StartFire() 
        {
            //_isTarget = (monsterScannerTest.nearestTarget == null) ? false : true;
        }
        private void RotateGunTowardsTarget()
        {
            // ��ó ���� �ִ��� Ȯ��
            if (monsterScannerTest.nearestTarget != null)
            {
                // ������ �Ÿ� �� ���� ���
                Vector3 targetDirection = monsterScannerTest.nearestTarget.position - transform.position;

                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                if (targetDirection.x < 0)
                {
                    sprite.flipX = false;
                    weaponManager.transform.localScale = new Vector3(1, 1, 1);
                    angle += 180;

                    if (angle > 360)
                        angle -= 360;
                }
                else
                {
                    sprite.flipX = true;
                    weaponManager.transform.localScale = new Vector3(-1, 1, 1);
                }

                weaponManager.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Weapon")
            {
                weaponManager.KeepGun(collision.gameObject.name);
                collision.gameObject.SetActive(false);
            }
        }
    }
}