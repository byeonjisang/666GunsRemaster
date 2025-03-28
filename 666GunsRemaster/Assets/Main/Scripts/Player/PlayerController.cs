using Character.Player.State;
using Gun;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
        public static PlayerController Instance => _instance;

        //외부 참조
        [SerializeField]
        private PlayerData playerData;
        private MonsterScannerTest monsterScannerTest;
        private WeaponManager weaponManager;
        public FloatingJoystick Joystick;
        [SerializeField]
        private Button dashButton;
        private Collider2D playerCollider;

        //플레이어 데이터
        public float Health {
            get { return _health; }
            set {
                _health = value;
                UIManager.Instance.UpdatePlayerHealth(_maxHealth, _health);
            } 
        }
        public float GetHp() { return _health; }
        public void SetHp(float damage) { 
            _health -= damage;
            UIManager.Instance.UpdatePlayerHealth(_maxHealth, _health);
            //피격 사운드
            SoundManager.instance.PlayEffectSoundOnce(7);

            StartCoroutine(Unbeatable());
        }
        public float CurrentSpeed { get; set; }
        public float MoveSpeed { get { return _moveSpeed; } set { } }
        public int DashCount {
            get { return _dashCount; } 
            set
            {
                _dashCount = value;
            }
        }
        public int CurrentDashCount
        {
            get { return _currentDashCount; }
            set
            {
                _currentDashCount = value;
                Debug.Log(_dashCount + " " + _currentDashCount);
                UIManager.Instance.PlayerDashUiInit(_dashCount);
                UIManager.Instance.UpdatePlayerDash(_dashCount, _currentDashCount);
            }
        }
        public float DashSpeed { get { return _dashSpeed; } }
        public float DashDuration { get { return _dashDuration; } }
        public float DashCooldown { get { return _dashCooldown; } }
        public float DashFillInTime { get { return _dashFillInTime; } }

        [SerializeField]
        private float _maxHealth;
        private float _health;                //체력
        private float _moveSpeed;           //이동속도
        private int _dashCount;           //대쉬횟수
        private int _currentDashCount;    //현재 대쉬횟수
        private float _dashSpeed;           //대쉬속도
        private float _dashDuration;        //대쉬지속시간
        private float _dashCooldown;        //대쉬쿨타임
        private float _cooldownTimeLeft;    //남은 대쉬쿨타임 시간
        private float _dashFillInTime;      //대쉬가 다시 차는 시간
        private bool _isCooldown = false;   //대쉬쿨타임 여부}

        public int CurrentWeaponIndex { private get; set; }
        //private bool _isFire = false;       //총 발사 여부
        //private bool _isFire = false;       //총 발사 여부
        private bool _isUnbeatable = false;   //무적 여부
        private Color hitEffect = new Color(1, 0, 0, 1); //피격시 색상

        private BoolReactiveProperty isDie = new BoolReactiveProperty(false); // 사망 상태를 ReactiveProperty로
        public IReadOnlyReactiveProperty<bool> IsDie => isDie; // 외부에서 읽기 전용으로 접근

        //public bool GetIsDie() { return isDie; }
        //public void SetIsDie(bool die) {  isDie = die; }

        //플레이어 버프 관련
        private int[] _buffLevel;

        //플레이어 컴포넌트
        private Rigidbody2D rigid;
        private Animator anim;
        private SpriteRenderer sprite;
        private Collider2D collider;

        //상태 패턴
        private IPlayerState _moveState, _stopState, _dashState, _dieState;
        private PlayerStateContext _playerStateContext;

        private void Awake()
        {
            if (Instance == null)
            {
                _instance = this;
            }

            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
            collider = GetComponent<Collider2D>();

            weaponManager = GetComponentInChildren<WeaponManager>();
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
            _dieState = gameObject.AddComponent<PlayerDieState>();

            _playerStateContext.Transition(_stopState);

            //대쉬 버튼 이벤트 등록
            dashButton.onClick.AddListener(StartDash);
        }

        //데이터 초기화
        public void StateInit()
        {
            _maxHealth = playerData.maxHealth;
            _health = _maxHealth;

            _moveSpeed = playerData.moveSpeed;
            _dashCount = playerData.dashCount;
            _currentDashCount = _dashCount;
            _dashSpeed = playerData.dashSpeed;
            _dashDuration = playerData.dashDuration;
            _dashCooldown = playerData.dashCooldown;
            _dashFillInTime = playerData.fillInTime;

            //대쉬 UI 초기화
            UIManager.Instance.PlayerDashUiInit(_dashCount);

            //버프 초기화
            _buffLevel = new int[System.Enum.GetValues(typeof(BuffType)).Length];
        }

        private void Update()
        {
            //죽었을 경우
            if (isDie.Value)
                return;

            //사망 판단
            if (_health <= 0 && !isDie.Value)
            {
                Die();
                _playerStateContext.Transition(_dieState);
                weaponManager.DeleteAllWeapon();
            }
        }

        public void ApplyBuff(string buffType)
        {
            IBuff buff = BuffManager.Instance.CreateBuff(buffType);

            int buffIndex = -1;
            switch (buffType)
            {
                case "Heal":
                    buffIndex = (int)BuffType.Heal;
                    break;
                case "SpeedUp":
                    buffIndex = (int)BuffType.SpeedUp;
                    break;
                case "BulletUp":
                    buffIndex = (int)BuffType.BulletUp;
                    break;
                case "ViewUp":
                    buffIndex = (int)BuffType.ViewUp;
                    break;
                case "DashCountUp":
                    buffIndex = (int)BuffType.DashCountUp;
                    PlayerDashState dashState = gameObject.GetComponent<PlayerDashState>();
                    dashState.DashCount += 1;
                    dashState.CurrentDashCount += 1;
                    break;
            }

            buff.ApplyBuff(_buffLevel[buffIndex]++);
        }

        private IEnumerator Unbeatable()
        {
            gameObject.layer = 8;
            _isUnbeatable = true;

            Color saveColor = sprite.color;
            sprite.color = hitEffect;
            yield return new WaitForSeconds(0.5f);
            sprite.color = saveColor;

            yield return new WaitForSeconds(1f);
            gameObject.layer = 3;
            _isUnbeatable = false;
        }

        //사망과 부활 함수 추가. 이는 isDie 변수를 외부에서 변경하기 위함.
        public void Die()
        {
            isDie.Value = true;
            if (playerCollider != null)
            {
                playerCollider.enabled = false; // 사망 시 Collider 비활성화
                _playerStateContext.Transition(_dieState);
            }
        }

        public void Revive()
        {
            isDie.Value = false;
            if (playerCollider != null)
            {
                //playerCollider.enabled = true; // 사망 시 Collider 비활성화

            }
        }

        private void FixedUpdate()
        {
            //죽었을 경우
            if (isDie.Value)
                return;


            //이동 및 정지 상태 판단
            if ((Joystick.Horizontal != 0 && Joystick.Vertical != 0))
            {
                _playerStateContext.Transition(_moveState);
            }
            else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                _playerStateContext.Transition(_moveState);
            }
            else
            {
                _playerStateContext.Transition(_stopState);
            }

            if (Input.GetButtonDown("Jump"))
            {
                //대쉬
                _playerStateContext.Transition(_dashState);
            }
        }

        //대쉬 시작
        private void StartDash()
        {
            _playerStateContext.Transition(_dashState);
        }

        public void ReversePlayer(bool reverse)
        {
            sprite.flipX = reverse;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Weapon")
            {
                weaponManager.KeepGun(collision.gameObject.name);
                collision.gameObject.SetActive(false);
                Debug.Log("총기 삭제");
            }
            
            if(collision.tag == "EnemyBullet")
            {
                if (_isUnbeatable)
                    return;

                float damage = collision.GetComponent<Gun.Bullet.PoliceBullet>().GetDamage();
                SetHp(damage);
            }
        }
    }
}