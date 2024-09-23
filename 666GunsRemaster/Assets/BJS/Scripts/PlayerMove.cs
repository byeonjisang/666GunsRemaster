using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Button dashBtn;
    public FloatingJoystick joyStick; // ���̽�ƽ �Է�
    public Image dashButtonImage;     // ��� ��ư�� �̹��� (���İ� ������)
    public float moveSpeed = 5f;      // �⺻ �̵� �ӵ�
    public float dashSpeed = 10f;     // ��� �ӵ�
    public float dashDuration = 0.5f; // ��� �ð�
    public float dashCooldown = 3f;   // ��� ��Ÿ��
    private bool isDashing = false;   // ��� ����
    private float dashTimeLeft;       // ���� ��� �ð�
    private float cooldownTimeLeft;   // ���� ��Ÿ�� �ð�
    private bool isCooldown = false;  // ��Ÿ�� ����
    private Rigidbody2D rb;

    SpriteRenderer spriter;
    Animator anim;

    [SerializeField] private GameObject clearImage;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // ��� ��ư�� OnClick �̺�Ʈ ���
        dashBtn.onClick.AddListener(StartDash);
    }

    void Update()
    {
        HandleMovement(); // �̵� �� ��� ó��
        HandleCooldown(); // ��Ÿ�� ó��
    }

    void HandleMovement()
    {
        // ���̽�ƽ �Է��� ����
        float joyStickInputX = joyStick.Horizontal;
        float joyStickInputY = joyStick.Vertical;

        // Ű���� �Է� ó��
        float keyboardInputX = Input.GetAxis("Horizontal"); // A/D or ��/�� Ű
        float keyboardInputY = Input.GetAxis("Vertical");   // W/S or ��/�� Ű

        // Ű����� ���̽�ƽ �Է��� ��ħ
        float finalInputX = joyStickInputX + keyboardInputX;
        float finalInputY = joyStickInputY + keyboardInputY;

        // �⺻ �̵� ó��
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                rb.velocity = new Vector2(finalInputX * dashSpeed, finalInputY * dashSpeed);
                dashTimeLeft -= Time.deltaTime;
            }
            else
            {
                // ��� ����
                isDashing = false;
                isCooldown = true; // ��� �� ��Ÿ�� ����
                cooldownTimeLeft = dashCooldown;
            }
        }
        else
        {
            rb.velocity = new Vector2(finalInputX * moveSpeed, finalInputY * moveSpeed);

            anim.SetFloat("Speed", rb.velocity.magnitude);

            //Sprite�� �ٶ󺸴� ���� ��ȯ.
            if (finalInputX != 0)
            {
                spriter.flipX = finalInputX < 0;
            }
        }
    }

    // ��� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    void StartDash()
    {
        if (!isDashing && !isCooldown) // ��� ���� �ƴϰ� ��Ÿ���� �ƴ� ���� ��� ����
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
        }
    }

    // ��Ÿ���� ó���ϰ� ��ư ���İ� ����
    void HandleCooldown()
    {
        if (isCooldown)
        {
            if (cooldownTimeLeft > 0)
            {
                cooldownTimeLeft -= Time.deltaTime;

                // ��Ÿ�ӿ� ���� ��ư ���İ� ���� (0.5 ~ 1f ���̷� ���� ����)
                float alphaValue = Mathf.Lerp(1f, 0.1f, cooldownTimeLeft / dashCooldown);
                SetButtonAlpha(alphaValue);
            }
            else
            {
                // ��Ÿ�� ����
                isCooldown = false;
                SetButtonAlpha(1f); // ��ư�� ������ �������ϰ�
            }
        }
    }

    // ��ư ���İ��� �����ϴ� �Լ�
    void SetButtonAlpha(float alpha)
    {
        Color color = dashButtonImage.color;
        color.a = alpha;
        dashButtonImage.color = color;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") // Ground �±��� ������Ʈ�� �浹 ��
        {
            GameManager.instance.Restart();
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.name == "StrongPoint") // Ground �±��� ������Ʈ�� �浹 ��
    //    {
    //        clearImage.SetActive(true);
    //    }
    //}
}
