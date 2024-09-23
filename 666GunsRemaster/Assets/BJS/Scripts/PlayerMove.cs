using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Button dashBtn;
    public FloatingJoystick joyStick; // 조이스틱 입력
    public Image dashButtonImage;     // 대시 버튼의 이미지 (알파값 조정용)
    public float moveSpeed = 5f;      // 기본 이동 속도
    public float dashSpeed = 10f;     // 대시 속도
    public float dashDuration = 0.5f; // 대시 시간
    public float dashCooldown = 3f;   // 대시 쿨타임
    private bool isDashing = false;   // 대시 여부
    private float dashTimeLeft;       // 남은 대시 시간
    private float cooldownTimeLeft;   // 남은 쿨타임 시간
    private bool isCooldown = false;  // 쿨타임 여부
    private Rigidbody2D rb;

    SpriteRenderer spriter;
    Animator anim;

    [SerializeField] private GameObject clearImage;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // 대시 버튼에 OnClick 이벤트 등록
        dashBtn.onClick.AddListener(StartDash);
    }

    void Update()
    {
        HandleMovement(); // 이동 및 대시 처리
        HandleCooldown(); // 쿨타임 처리
    }

    void HandleMovement()
    {
        // 조이스틱 입력을 받음
        float joyStickInputX = joyStick.Horizontal;
        float joyStickInputY = joyStick.Vertical;

        // 키보드 입력 처리
        float keyboardInputX = Input.GetAxis("Horizontal"); // A/D or ←/→ 키
        float keyboardInputY = Input.GetAxis("Vertical");   // W/S or ↑/↓ 키

        // 키보드와 조이스틱 입력을 합침
        float finalInputX = joyStickInputX + keyboardInputX;
        float finalInputY = joyStickInputY + keyboardInputY;

        // 기본 이동 처리
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                rb.velocity = new Vector2(finalInputX * dashSpeed, finalInputY * dashSpeed);
                dashTimeLeft -= Time.deltaTime;
            }
            else
            {
                // 대시 종료
                isDashing = false;
                isCooldown = true; // 대시 후 쿨타임 시작
                cooldownTimeLeft = dashCooldown;
            }
        }
        else
        {
            rb.velocity = new Vector2(finalInputX * moveSpeed, finalInputY * moveSpeed);

            anim.SetFloat("Speed", rb.velocity.magnitude);

            //Sprite가 바라보는 방향 전환.
            if (finalInputX != 0)
            {
                spriter.flipX = finalInputX < 0;
            }
        }
    }

    // 대시 버튼을 눌렀을 때 호출되는 함수
    void StartDash()
    {
        if (!isDashing && !isCooldown) // 대시 중이 아니고 쿨타임이 아닐 때만 대시 가능
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
        }
    }

    // 쿨타임을 처리하고 버튼 알파값 조정
    void HandleCooldown()
    {
        if (isCooldown)
        {
            if (cooldownTimeLeft > 0)
            {
                cooldownTimeLeft -= Time.deltaTime;

                // 쿨타임에 따른 버튼 알파값 변경 (0.5 ~ 1f 사이로 투명도 조정)
                float alphaValue = Mathf.Lerp(1f, 0.1f, cooldownTimeLeft / dashCooldown);
                SetButtonAlpha(alphaValue);
            }
            else
            {
                // 쿨타임 종료
                isCooldown = false;
                SetButtonAlpha(1f); // 버튼을 완전히 불투명하게
            }
        }
    }

    // 버튼 알파값을 설정하는 함수
    void SetButtonAlpha(float alpha)
    {
        Color color = dashButtonImage.color;
        color.a = alpha;
        dashButtonImage.color = color;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") // Ground 태그의 오브젝트와 충돌 시
        {
            GameManager.instance.Restart();
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.name == "StrongPoint") // Ground 태그의 오브젝트와 충돌 시
    //    {
    //        clearImage.SetActive(true);
    //    }
    //}
}
