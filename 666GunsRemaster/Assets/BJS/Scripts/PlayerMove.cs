using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;      // 좌우 이동 속도
    public float jumpForce = 10f;     // 점프 힘
    private bool isGrounded = true;   // 땅에 닿았는지 확인
    private Rigidbody2D rb;           // Rigidbody2D 컴포넌트

    //조이스틱 작동을 위한 선언
    public FixedJoystick joyStick;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D 가져오기
    }

    void Update()
    {
        Move();
        //Jump();
    }

    void Move()
    {
        float moveInputX = Input.GetAxis("Horizontal");  // 좌우 입력 받기 (A, D or 왼쪽, 오른쪽 화살표)
        float moveInputY = Input.GetAxis("Vertical");  // 좌우 입력 받기 (A, D or 왼쪽, 오른쪽 화살표)

        //조이스틱 입력
        float joyStickInputX = joyStick.Horizontal;
        float joyStickInputY = joyStick.Vertical;
        
        //rb.velocity = new Vector2(moveInputX * moveSpeed, moveInputY * moveSpeed);  // x축으로 이동 (y축 속도 유지)
        
        //조이스틱 이동
        rb.velocity = new Vector2(joyStickInputX * moveSpeed, joyStickInputY * moveSpeed);  // x축으로 이동 (y축 속도 유지)
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)  // 점프 키 (스페이스바 기본) 입력 시
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // 점프 힘 추가
            isGrounded = false;  // 점프했으므로 지상에서 떨어짐
        }
    }

    // 바닥에 닿았는지 확인하는 트리거
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.name == "Floor")  // Ground 태그의 오브젝트와 충돌 시
        //{
        //    isGrounded = true;  // 다시 땅에 있음
        //}
        //else if (collision.gameObject.name == "Goal")  // Goal 태그의 오브젝트와 충돌 시
        //{
        //    //게임 재시작
        //    GameManager.instance.Restart();
        //}
    }
}
