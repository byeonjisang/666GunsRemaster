using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //조이스틱 값 받아오기
        direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        player.Move(direction);
    }

    public void Attack()
    {
        WeaponManager.Instance.Attack();
    }
}