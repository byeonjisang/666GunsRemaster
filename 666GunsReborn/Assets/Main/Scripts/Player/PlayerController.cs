using System;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : Singleton<PlayerController>
{
    [Header("Player Type")]
    [SerializeField]
    private PlayerType playerType;

    #region Player Components
    [Header("Player Componenets")]
    [SerializeField]
    private Joystick joystick;
    #endregion

    private Vector3 direction = Vector3.zero;

    public UnityEvent<Vector3> OnMovePress;
    public UnityEvent OnAttackPress;
    public UnityEvent OnAttackReleased;
    public UnityEvent<Vector3> OnDashPress;


    // 지울 것것
    private Player player;

    private void Start()
    {
        switch (PlayerManager.Instance.PlayerType)
        {
            case PlayerType.Attack:
                player = gameObject.AddComponent<AttackPlayer>();
                break;
            case PlayerType.Defense:
                player = gameObject.AddComponent<DefensePlayer>();
                break;
            case PlayerType.Balance:
                player = gameObject.AddComponent<BalancePlayer>();
                break;
            default:
                Debug.LogError("Invalid Player Type");
                break;
        }
        player.Initialized();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Dash();
    }

    #region Player Move
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //���̽�ƽ �� �޾ƿ���
        direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        //Ű���� �Է� �� �޾ƿ���
        direction += new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //����ȭ
        direction.Normalize();

        //�÷��̾� �̵�
        //PlayerManager.Instance.player.HandleInput(direction);
        OnMovePress?.Invoke(direction);
    }
    #endregion

    #region Player Attack
    public void OnClickAttack()
    {
        //player.StartAttack();
        //PlayerManager.Instance.player.attackSystem.RequestAttack();
        OnAttackPress?.Invoke();
    }

    public void OffClickAttack()
    {
        //player.StopAttack();
        //PlayerManager.Instance.player.attackSystem.CancelAttackRequest();
        OnAttackReleased?.Invoke();
    }

    public void FireBullet(){
        WeaponManager.Instance.Attack();
    }
    #endregion

    #region Player Dash
    public void Dash()
    {
        //PlayerManager.Instance.player.Dash(direction);
        OnDashPress?.Invoke(direction);
    }
#endregion
}