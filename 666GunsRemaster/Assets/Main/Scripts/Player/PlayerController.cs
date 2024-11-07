using Character.Player.State;
using Gun;
using System.Collections;
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
        public OverHit overHit;
        private WeaponManager weaponManager;
        public FloatingJoystick Joystick;
        [SerializeField]
        private Button dashButton;

        //�÷��̾� ������
        public float Health { get { return _health; } }
        public float GetHp() { return _health; }
        public void SetHp(float damage) { _health -= damage; }
        public float CurrentSpeed { get; set; }
        public float MoveSpeed { get { return _moveSpeed; } }
        public float DashSpeed { get { return _dashSpeed; } }
        public float DashDuration { get { return _dashDuration; } }
        public float DashCooldown { get { return _dashCooldown; } }

        [SerializeField]
        private float _health;                //ü��
        private float _moveSpeed;           //�̵��ӵ�
        private float _dashSpeed;           //�뽬�ӵ�
        private float _dashDuration;        //�뽬���ӽð�
        private float _dashCooldown;        //�뽬��Ÿ��
        private float _cooldownTimeLeft;    //���� �뽬��Ÿ�� �ð�
        private bool _isCooldown = false;   //�뽬��Ÿ�� ����

        [Header("OverHit")]
        [SerializeField]
        private float _overHitTime;         //������Ʈ �ð�
        public bool IsOverHit;
        [SerializeField]
        private float _currentOverHitTime;  //���� ������Ʈ �ð�
        private float _overHitCount;        //������Ʈ ī��Ʈ
        [SerializeField]
        private float _currentOverHitCount; //���� ������Ʈ ī��Ʈ
        private float _startDecreaseTime;   //������Ʈ ���� ���� �ð�
        private bool _isDecrease = false;  //������Ʈ ���� ����

        private bool _isFire = false;       //�� �߻� ����
        public bool IsTarget = false;     //Ÿ�� ���� ����
        private bool isDie = false;

        //�÷��̾� ������Ʈ
        private Rigidbody2D rigid;
        private Animator anim;
        private SpriteRenderer sprite;

        //���� ����
        private IPlayerState _moveState, _stopState, _dashState, _dieState;
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

            weaponManager = GetComponentInChildren<WeaponManager>();
            monsterScannerTest = GetComponent<MonsterScannerTest>();
            overHit = GetComponentInChildren<OverHit>();

            StateInit();    //������ �ʱ�ȭ
        }

        private void Start()
        {
            //���� ���� �ʱ�ȭ
            _playerStateContext = new PlayerStateContext(this);

            _moveState = gameObject.AddComponent<PlayerMoveState>();
            _stopState = gameObject.AddComponent<PlayerStopState>();
            _dashState = gameObject.AddComponent<PlayerDashState>();
            _dieState = gameObject.AddComponent<PlayerDieState>();

            _playerStateContext.Transition(_stopState);

            //�뽬 ��ư �̺�Ʈ ���
            dashButton.onClick.AddListener(StartDash);

            //������Ʈ ��Ȱ��ȭ
            StartCoroutine(DecreaseGaugeOverHit());
            overHit.gameObject.SetActive(false);
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
            if (Input.GetKeyDown(KeyCode.V))
            {
                _health -= 1;
                Debug.Log("������ ����");
            }
                

            //�뽬 ��Ÿ�� ���
            if (_isCooldown)
            {
                _cooldownTimeLeft -= Time.deltaTime;
                if (_cooldownTimeLeft <= 0)
                {
                    _isCooldown = false;
                }
            }

            //������Ʈ �ð� ���
            if (IsOverHit)
            {
                _currentOverHitTime += Time.deltaTime;
                if (_currentOverHitTime >= _overHitTime)
                {
                    IsOverHit = false;
                    _currentOverHitTime = 0;
                    _currentOverHitCount = 0;
                }
            }

            if(_health <= 0 && !isDie)
            {
                isDie = true;
                _playerStateContext.Transition(_dieState);
                weaponManager.DeleteAllWeapon();
            }
        }

        private void FixedUpdate()
        {
            //�̵� �� ���� ���� �Ǵ�
            if ((Joystick.Horizontal != 0 && Joystick.Vertical != 0))
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
            _currentOverHitCount += 1f;
            if (_currentOverHitCount >= _overHitCount)
            {
                IsOverHit = true;
                overHit.gameObject.SetActive(true);
            }
            else
            {
                _isDecrease = false;
                StartCoroutine(ResetOverHitFlag());
            }
        }
        private IEnumerator ResetOverHitFlag()
        {
            yield return new WaitForSeconds(2f);
            _isDecrease = true;
        }
        private IEnumerator DecreaseGaugeOverHit()
        {
            while (true)
            {
                if (_isDecrease && _currentOverHitCount > 0)
                {
                    _currentOverHitCount = Mathf.Max(_currentOverHitCount - 1f, 0);
                }
                yield return new WaitForSeconds(1f);
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

                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg + 270;
                if (targetDirection.x < 0)
                {
                    sprite.flipX = false;
                    weaponManager.transform.localScale = new Vector3(1, 1, 1);

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

                if (weaponManager.transform.localScale.x == -1)
                    weaponManager.transform.rotation = Quaternion.Euler(0, 0, -90);
                else
                    weaponManager.transform.rotation = Quaternion.Euler(0, 0, 90);
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

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Position")
            {
                GameManager.instance.Timer();
            }
        }
    }
}