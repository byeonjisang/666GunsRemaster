using Character.Player.State;
using Gun;
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

        //�ܺ� ����
        [SerializeField]
        private PlayerData playerData;
        private MonsterScannerTest monsterScannerTest;
        public OverHit overHit;
        private WeaponManager weaponManager;
        public FloatingJoystick Joystick;
        [SerializeField]
        private Button dashButton;
        private Collider2D playerCollider;

        //�÷��̾� ������
        public float Health { get { return _health; } }
        public float GetHp() { return _health; }
        public void SetHp(float damage) { 
            _health -= damage;
            UIManager.Instance.UpdatePlayerHealth(_maxHealth, _health);
            //�ǰ� ����

            StartCoroutine(Unbeatable());
        }
        public float CurrentSpeed { get; set; }
        public float MoveSpeed { get { return _moveSpeed; } }
        public int DashCount { get { return _dashCount; } }
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
        private bool _isUnbeatable = false; //���� ����
        private Color hitEffect = new Color(1, 0, 0, 1); //�ǰݽ� ����

        private BoolReactiveProperty isDie = new BoolReactiveProperty(false); // ��� ���¸� ReactiveProperty��
        public IReadOnlyReactiveProperty<bool> IsDie => isDie; // �ܺο��� �б� �������� ����

        //public bool GetIsDie() { return isDie; }
        //public void SetIsDie(bool die) {  isDie = die; }

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
            _maxHealth = playerData.maxHealth;
            _health = _maxHealth;

            _moveSpeed = playerData.moveSpeed;
            _dashCount = playerData.dashCount;
            _currentDashCount = _dashCount;
            _dashSpeed = playerData.dashSpeed;
            _dashDuration = playerData.dashDuration;
            _dashCooldown = playerData.dashCooldown;
            _dashFillInTime = playerData.fillInTime;

            _overHitTime = playerData.overHitTime;
            _overHitCount = playerData.overHitCount;

            UIManager.Instance.PlayerDashUiInit(_dashCount);
        }

        private void Update()
        {
            //�׾��� ���
            if (isDie.Value)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
                StartDash();

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

            //��� �Ǵ�
            if(_health <= 0 && !isDie.Value)
            {
                Die();
                _playerStateContext.Transition(_dieState);
                weaponManager.DeleteAllWeapon();
            }
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
            else
            {
                _playerStateContext.Transition(_stopState);
            }
        }

        //�뽬 ����
        private void StartDash()
        {
            _playerStateContext.Transition(_dashState);
        }

        public void OverHit()
        {
            _currentOverHitCount += 1f;
            if (_currentOverHitCount >= _overHitCount)
            {
                IsOverHit = true;
                //������Ʈ ����

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