using Character.Player.State;
using Gun;
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
        private WeaponManager weaponManager;
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
        public bool IsOverHit { get { return _isOverHit; } }

        private int _health;                //체력
        private float _moveSpeed;           //이동속도
        private float _dashSpeed;           //대쉬속도
        private float _dashDuration;        //대쉬지속시간
        private float _dashCooldown;        //대쉬쿨타임
        private float _cooldownTimeLeft;    //남은 쿨타임 시간
        private bool _isCooldown = false;   //쿨타임 여부
        private float _overHitTime;         //오버히트 시간
        private float _currentOverHitTime;  //현재 오버히트 시간
        private float _overHitCount;        //오버히트 카운트
        private float _currentOverHitCount; //현재 오버히트 카운트
        private bool _isOverHit = false;

        private bool _isFire = false;       //총 발사 여부
        public bool IsTarget = false;     //타켓 존재 여부

        //플레이어 컴포넌트
        private Rigidbody2D rigid;
        private Animator anim;
        private SpriteRenderer sprite;

        //상태 패턴
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
            //대쉬 쿨타임 계산
            if (_isCooldown)
            {
                _cooldownTimeLeft -= Time.deltaTime;
                if(_cooldownTimeLeft <= 0)
                {
                    _isCooldown = false;
                }
            }

            //오버히트 시간 계산
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
            //이동 및 정지 상태 판단
            if((Joystick.Horizontal != 0 && Joystick.Vertical != 0))
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
            _currentOverHitCount += 0.1f;
            if(_currentOverHitCount >= _overHitCount)
            {
                _isOverHit = true;
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