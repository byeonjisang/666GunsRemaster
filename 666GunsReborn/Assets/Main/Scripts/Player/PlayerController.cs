using System;
using UnityEngine;
using UnityEngine.Events;
using Weapons;


public class PlayerController : MonoBehaviour
{
    #region Player Components
    [Header("Player Componenets")]
    [SerializeField]
    private Joystick joystick;
    #endregion

    private Vector3 direction = Vector3.zero;

    private void Update()
    {
        // 컴퓨터 테스트용 코드
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
        // 조이스틱 입력에 따른 방향 벡터
        direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        // 키보드 입력에 따른 방향 벡터
        direction += new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // 이동
        direction.Normalize();

        // 이동 방향 전달
        PlayerActionEvent.OnMovePress?.Invoke(direction);
    }
    #endregion

    #region Player Attack
    public void OnClickAttack()
    {
        PlayerActionEvent.OnAttackPress?.Invoke();
        WeaponManager1.Instance.OnFire();
    }

    public void OffClickAttack()
    {
        PlayerActionEvent.OnAttackReleased?.Invoke();
        WeaponManager1.Instance.OffFire();
    }
    #endregion

    #region Player Dash
    public void Dash()
    {
        Debug.Log("Dash");
        PlayerActionEvent.OnDashPress?.Invoke();
    }
#endregion
}