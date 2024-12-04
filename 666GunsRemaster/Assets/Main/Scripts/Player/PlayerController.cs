using Character.Player.State;
using Gun;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

namespace Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
        public static PlayerController Instance => _instance;

        //�ܺ� ����
        [SerializeField]
        private PlayerData playerData;
        private MonsterScannerTest monsterScannerTest;
        public OverHit overHit;
        private WeaponManager weaponManager;
        private OverhitManager overhitManager;
        public FloatingJoystick Joystick;
        [SerializeField]
        private Button dashButton;
        private Collider2D playerCollider;

        //�÷��̾� ������
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
            //�ǰ� ����

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
        private float _health;                //ü��
        private float _moveSpeed;           //�̵��ӵ�
        private int _dashCount;           //�뽬Ƚ��
        private int _currentDashCount;    //���� �뽬Ƚ��
        private float _dashSpeed;           //�뽬�ӵ�
        private float _dashDuration;        //�뽬���ӽð�
        private float _dashCooldown;        //�뽬��Ÿ��
        private float _cooldownTimeLeft;    //���� �뽬��Ÿ�� �ð�
        private float _dashFillInTime;      //�뽬�� �ٽ� ���� �ð�
        private bool _isCooldown = false;   //�뽬��Ÿ�� ����

        public bool[] IsOverHit { get { return overhitManager._isOverhit; } }

        public int CurrentWeaponIndex { private get; set; }
        //private bool _isFire = false;       //�� �߻� ����
        //private bool _isFire = false;       //�� �߻� ����
        private bool _isUnbeatable = false;   //���� ����
        private Color hitEffect = new Color(1, 0, 0, 1); //�ǰݽ� ����

        private BoolReactiveProperty isDie = new BoolReactiveProperty(false); // ��� ���¸� ReactiveProperty��
        public IReadOnlyReactiveProperty<bool> IsDie => isDie; // �ܺο��� �б� �������� ����

        //public bool GetIsDie() { return isDie; }
        //public void SetIsDie(bool die) {  isDie = die; }

        //�÷��̾� ���� ����
        private int[] _buffLevel;

        //�÷��̾� ������Ʈ
        private Rigidbody2D rigid;
        private Animator anim;
        private SpriteRenderer sprite;
        private Collider2D collider;

        //���� ����
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
            overHit = GetComponentInChildren<OverHit>();
            overhitManager = gameObject.AddComponent<OverhitManager>();

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
        }

        //������ �ʱ�ȭ
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

            //������Ʈ ����
            overhitManager.OverhitInit(playerData, overHit);

            //�뽬 UI �ʱ�ȭ
            UIManager.Instance.PlayerDashUiInit(_dashCount);

            //���� �ʱ�ȭ
            _buffLevel = new int[System.Enum.GetValues(typeof(BuffType)).Length];
        }

        private void Update()
        {
            //�׾��� ���
            if (isDie.Value)
                return;

            //��� �Ǵ�
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

        //����� ��Ȱ �Լ� �߰�. �̴� isDie ������ �ܺο��� �����ϱ� ����.
        public void Die()
        {
            isDie.Value = true;
            if (playerCollider != null)
            {
                playerCollider.enabled = false; // ��� �� Collider ��Ȱ��ȭ
            }
        }

        public void Revive()
        {
            isDie.Value = false;
            if (playerCollider != null)
            {
                //playerCollider.enabled = true; // ��� �� Collider ��Ȱ��ȭ

            }
        }

        private void FixedUpdate()
        {
            //�׾��� ���
            if (isDie.Value)
                return;


            //�̵� �� ���� ���� �Ǵ�
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
                //�뽬
                _playerStateContext.Transition(_dashState);
            }
        }

        //�뽬 ����
        private void StartDash()
        {
            _playerStateContext.Transition(_dashState);
        }

        public void OverHit()
        {
            overhitManager.IncreaseOverhitGauge(CurrentWeaponIndex);
        }
        
        //������Ʈ ����
        public void OverhitReset(int index)
        {
            overhitManager.OverhitReset(index);
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
                Debug.Log("�ѱ� ����");
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