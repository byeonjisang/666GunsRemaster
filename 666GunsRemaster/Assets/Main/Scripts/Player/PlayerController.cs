using Character.Player.State;
using Gun;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        //�ܺ� ����
        [SerializeField]
        private PlayerData playerData;
        private MonsterScannerTest monsterScannerTest;
        private WeaponManager weaponManager;
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
        public bool IsOverHit { get { return _isOverHit; } }

        private int _health;                //ü��
        private float _moveSpeed;           //�̵��ӵ�
        private float _dashSpeed;           //�뽬�ӵ�
        private float _dashDuration;        //�뽬���ӽð�
        private float _dashCooldown;        //�뽬��Ÿ��
        private float _cooldownTimeLeft;    //���� ��Ÿ�� �ð�
        private bool _isCooldown = false;   //��Ÿ�� ����
        private float _overHitTime;         //������Ʈ �ð�
        private float _currentOverHitTime;  //���� ������Ʈ �ð�
        private float _overHitCount;        //������Ʈ ī��Ʈ
        private float _currentOverHitCount; //���� ������Ʈ ī��Ʈ
        private bool _isOverHit = false;

        private bool _isFire = false;       //�� �߻� ����
        public bool IsTarget = false;     //Ÿ�� ���� ����

        //�÷��̾� ������Ʈ
        private Rigidbody2D rigid;
        private Animator anim;
        private SpriteRenderer sprite;

        //���� ����
        private IPlayerState _moveState, _stopState, _fireState, _dashState;
        private PlayerStateContext _playerStateContext;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

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
        }

        //������ �ʱ�ȭ
        public void StateInit()
        {
            _health = playerData.maxHealth;
            _moveSpeed = playerData.moveSpeed;
            _dashSpeed = playerData.dashSpeed;
            _dashDuration = playerData.dashDuration;
            _dashCooldown = playerData.dashCooldown;
            _overHitTime = playerData.overHitTime;
            _overHitCount = playerData.overHitCount;
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

            //������Ʈ �ð� ���
            if (_isOverHit)
            {
                _overHitTime += Time.fixedDeltaTime;
                if(_overHitTime >= _currentOverHitTime)
                {
                    _isOverHit = false;
                    _overHitTime = 0;
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

            _playerStateContext.Transition(_dashState);

            _cooldownTimeLeft = _dashCooldown;
            _isCooldown = true;
            anim.SetBool("IsDash", true);
        }
       
        public void OverHit()
        {
            _currentOverHitCount += 0.1f;
            if(_currentOverHitCount >= _overHitCount)
            {
                _isOverHit = true;
            }
        }

        private void RotateGunTowardsTarget()
        {
            // ��ó ���� �ִ��� Ȯ��
            if (monsterScannerTest.nearestTarget != null)
            {
                IsTarget = true;

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
            else
            {
                IsTarget = false;
                weaponManager.transform.rotation = Quaternion.identity;
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