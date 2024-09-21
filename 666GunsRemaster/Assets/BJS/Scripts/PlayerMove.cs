using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;      // �¿� �̵� �ӵ�
    public float jumpForce = 10f;     // ���� ��
    private bool isGrounded = true;   // ���� ��Ҵ��� Ȯ��
    private Rigidbody2D rb;           // Rigidbody2D ������Ʈ

    //���̽�ƽ �۵��� ���� ����
    public FixedJoystick joyStick;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D ��������
    }

    void Update()
    {
        Move();
        //Jump();
    }

    void Move()
    {
        float moveInputX = Input.GetAxis("Horizontal");  // �¿� �Է� �ޱ� (A, D or ����, ������ ȭ��ǥ)
        float moveInputY = Input.GetAxis("Vertical");  // �¿� �Է� �ޱ� (A, D or ����, ������ ȭ��ǥ)

        //���̽�ƽ �Է�
        float joyStickInputX = joyStick.Horizontal;
        float joyStickInputY = joyStick.Vertical;
        
        //rb.velocity = new Vector2(moveInputX * moveSpeed, moveInputY * moveSpeed);  // x������ �̵� (y�� �ӵ� ����)
        
        //���̽�ƽ �̵�
        rb.velocity = new Vector2(joyStickInputX * moveSpeed, joyStickInputY * moveSpeed);  // x������ �̵� (y�� �ӵ� ����)
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)  // ���� Ű (�����̽��� �⺻) �Է� ��
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // ���� �� �߰�
            isGrounded = false;  // ���������Ƿ� ���󿡼� ������
        }
    }

    // �ٴڿ� ��Ҵ��� Ȯ���ϴ� Ʈ����
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.name == "Floor")  // Ground �±��� ������Ʈ�� �浹 ��
        //{
        //    isGrounded = true;  // �ٽ� ���� ����
        //}
        //else if (collision.gameObject.name == "Goal")  // Goal �±��� ������Ʈ�� �浹 ��
        //{
        //    //���� �����
        //    GameManager.instance.Restart();
        //}
    }
}
