using UnityEngine;


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

    private Player player;

    private void Start()
    {
        switch (playerType)
        {
            case PlayerType.Player1:
                player = gameObject.AddComponent<Player1>();
                break;
            case PlayerType.Player2:
                player = gameObject.AddComponent<Player2>();
                break;
        }

        player.Init();
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
        player.HandleInput(direction);
    }
#endregion

#region Player Attack
    public void OnClickAttack()
    {
        //player.StartAttack();
        player.attackSystem.RequestAttack();
    }

    public void OffClickAttakc()
    {
        //player.StopAttack();
        player.attackSystem.CancelAttackRequest();
    }

    public void FireBullet(){
        WeaponManager.Instance.Attack();
    }
#endregion

#region Player Dash
    public void Dash()
    {
        player.Dash(direction);
    }
#endregion
}