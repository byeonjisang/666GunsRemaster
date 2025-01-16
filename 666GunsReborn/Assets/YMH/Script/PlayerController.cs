using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Player State
    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Skill,
        Dead
    }
    #endregion

    #region Player Components
    [SerializeField]
    private Joystick joystick;
    private Player_InputSystem inputSystem;
    private Animator anim;
    private Rigidbody rigid;
    #endregion

    private PlayerState state;
    private Vector3 direction = Vector3.zero;
    private float moveSpeed = 10;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        inputSystem = new Player_InputSystem();
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        state = PlayerState.Idle;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        //애니메이션 적용
        anim.SetFloat("Speed", direction.magnitude);
    }

    private void Move()
    {
        //조이스틱 값 받아오기
        direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

        //속도 계산
        float currentMoveSpeed = moveSpeed;
        //방향 전환
        LookAt();
        //속도값 적용
        rigid.velocity = direction * currentMoveSpeed;
    }

    private void LookAt()
    {
        if(direction != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            rigid.rotation = targetAngle;
        }
    }

    //public void OnMoveInput(InputAction.CallbackContext context)
    //{
    //    Vector2 input = context.ReadValue<Vector2>();
    //    direction = new Vector3(input.x, 0f, input.y);
    //}
}
