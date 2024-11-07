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

        //외부 참조
        [SerializeField]
        private PlayerData playerData;
        private MonsterScannerTest monsterScannerTest;
        public OverHit overHit;
        private WeaponManager weaponManager;
        public FloatingJoystick Joystick;
        [SerializeField]
        private Button dashButton;

        //플레이어 데이터
        public float Health { get { return _health; } }
        public float GetHp() { return _health; }
        public void SetHp(float damage) { _health -= damage; }
        public float CurrentSpeed { get; set; }
        public float MoveSpeed { get { return _moveSpeed; } }
        public float DashSpeed { get { return _dashSpeed; } }
        public float DashDuration { get { return _dashDuration; } }
        public float DashCooldown { get { return _dashCooldown; } }

        [SerializeField]
        private float _health;                //체력
        private float _moveSpeed;           //이동속도
        private float _dashSpeed;           //대쉬속도
        private float _dashDuration;        //대쉬지속시간
        private float _dashCooldown;        //대쉬쿨타임
        private float _cooldownTimeLeft;    //남은 대쉬쿨타임 시간
        private bool _isCooldown = false;   //대쉬쿨타임 여부

        [Header("OverHit")]
        [SerializeField]
        private float _overHitTime;         //오버히트 시간
        public bool IsOverHit;
        [SerializeField]
        private float _currentOverHitTime;  //현재 오버히트 시간
        private float _overHitCount;        //오버히트 카운트
        [SerializeField]
        private float _currentOverHitCount; //현재 오버히트 카운트
        private float _startDecreaseTime;   //오버히트 감소 시작 시간
        private bool _isDecrease = false;  //오버히트 감소 여부

        private bool _isFire = false;       //총 발사 여부
        public bool IsTarget = false;     //타켓 존재 여부
        private bool isDie = false;

        //플레이어 컴포넌트
        private Rigidbody2D rigid;
        private Animator anim;
        private SpriteRenderer sprite;

        //상태 패턴
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

            StateInit();    //데이터 초기화
        }

        private void Start()
        {
            //상태 패턴 초기화
            _playerStateContext = new PlayerStateContext(this);

            _moveState = gameObject.AddComponent<PlayerMoveState>();
            _stopState = gameObject.AddComponent<PlayerStopState>();
            _dashState = gameObject.AddComponent<PlayerDashState>();
            _dieState = gameObject.AddComponent<PlayerDieState>();

            _playerStateContext.Transition(_stopState);

            //대쉬 버튼 이벤트 등록
            dashButton.onClick.AddListener(StartDash);

            //오버히트 비활성화
            StartCoroutine(DecreaseGaugeOverHit());
            overHit.gameObject.SetActive(false);
        }

        //데이터 초기화
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
                Debug.Log("데미지 받음");
            }
                

            //대쉬 쿨타임 계산
            if (_isCooldown)
            {
                _cooldownTimeLeft -= Time.deltaTime;
                if (_cooldownTimeLeft <= 0)
                {
                    _isCooldown = false;
                }
            }

            //오버히트 시간 계산
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
            //이동 및 정지 상태 판단
            if ((Joystick.Horizontal != 0 && Joystick.Vertical != 0))
            {
                _playerStateContext.Transition(_moveState);
            }
            else
            {
                _playerStateContext.Transition(_stopState);
            }

            RotateGunTowardsTarget();

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
            // 근처 적이 있는지 확인
            if (monsterScannerTest.nearestTarget != null)
            {
                IsTarget = true;

                // 적과의 거리 및 방향 계산
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