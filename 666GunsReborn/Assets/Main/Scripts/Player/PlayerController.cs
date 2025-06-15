using System;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    #region Player Components
    [Header("Player Componenets")]
    [SerializeField]
    private Joystick joystick;
    #endregion

    private Vector3 direction = Vector3.zero;

    
    public Action<Vector3> OnMovePress;
    public Action OnAttackPress;
    public Action OnAttackReleased;
    public Action<Vector3> OnDashPress;

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
        OnMovePress?.Invoke(direction);
    }
    #endregion

    #region Player Attack
    public void OnClickAttack()
    {
        OnAttackPress?.Invoke();
    }

    public void OffClickAttack()
    {
        OnAttackReleased?.Invoke();
    }

    public void FireBullet(){
        WeaponManager.Instance.Attack();
    }
    #endregion

    #region Player Dash
    public void Dash()
    {
        OnDashPress?.Invoke(direction);
    }
#endregion
}