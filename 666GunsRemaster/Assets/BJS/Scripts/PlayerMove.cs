using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;      // �¿� �̵� �ӵ�
    public float jumpForce = 10f;     // ���� ��
    private bool isGrounded = true;   // ���� ��Ҵ��� Ȯ��
    private Rigidbody2D rb;           // Rigidbody2D ������Ʈ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D ��������
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");  // �¿� �Է� �ޱ� (A, D or ����, ������ ȭ��ǥ)
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);  // x������ �̵� (y�� �ӵ� ����)
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
        if (collision.gameObject.name == "Floor")  // Ground �±��� ������Ʈ�� �浹 ��
        {
            isGrounded = true;  // �ٽ� ���� ����
        }
        else if (collision.gameObject.name == "Goal")  // Goal �±��� ������Ʈ�� �浹 ��
        {
            //���� �����
            GameManager.instance.Restart();
        }
    }
}