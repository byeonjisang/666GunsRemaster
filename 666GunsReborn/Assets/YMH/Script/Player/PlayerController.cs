using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.CanvasScaler;

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

    public Vector3 direction = Vector3.zero;

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

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //조이스틱 값 받아오기
        direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        //키보드 입력 값 받아오기
        direction += new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //정규화
        direction.Normalize();

        player.Move(direction);
    }

    #region Player Attack
    public void OnClickAttack()
    {
        player.StartAttack();
    }

    public void OffClickAttakc()
    {
        player.StopAttack();
    }

    public void FireBullet()
    {
        WeaponManager.Instance.Attack();
    }
    #endregion

    public void Dash()
    {
        player.Dash();
    }
}