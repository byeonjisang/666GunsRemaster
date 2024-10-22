using Gun;
using Gun.Bullet;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Police2Stats : MonoBehaviour
{
    public Police2 police2;
    public Transform firePoint;  // 총알이 발사될 위치
    public GameObject bulletPrefab;  // 발사할 총알 프리팹
    public LayerMask playerLayer;    // 플레이어가 속한 레이어

    public float attackCooldown = 1f;    // 공격 간격
    private float lastAttackTime;

    private Transform player;
    private SpriteRenderer sprite;

    public SpriteRenderer gunSprite;

    private Animator animator;

    [SerializeField]
    private bool isInAttackRange = false;

    private bool isDead = false;

    //길찾기 적용
    NavMeshAgent agent;

    // 외부에서 Police2 데이터를 받아서 설정하는 메서드
    public void SetData(Police2 data)
    {
        police2 = data;
    }

    // 몬스터의 정보를 디버그 하기 위함.
    public void DebugMonsterInfo()
    {
        //Debug.Log("몬스터 이름 :: " + police2.GetMonsterName);
        //Debug.Log("몬스터 체력 :: " + police2.GetHp);
        //Debug.Log("몬스터 공격력 :: " + police2.GetDamage);
        //Debug.Log("몬스터 시야 :: " + police2.GetSightRange);
        //Debug.Log("몬스터 이동속도 :: " + police2.GetMoveSpeed);
        //Debug.Log("몬스터 사정거리 :: " + police2.GetAttackRange);
    }

    //// 복사 메서드
    //public Police2Stats Clone(GameObject newObject)
    //{
    //    Police2Stats clone = newObject.AddComponent<Police2Stats>();
    //    clone.animator = this.animator;

    //    // NavMeshAgent 재설정
    //    NavMeshAgent agent = newObject.GetComponent<NavMeshAgent>();

    //    if (agent == null)
    //    {
    //        agent = newObject.AddComponent<NavMeshAgent>();  // NavMeshAgent가 없으면 추가
    //    }

    //    // 기존 agent 설정 복사
    //    agent.speed = police2.GetMoveSpeed;
    //    agent.updateRotation = false;
    //    agent.updateUpAxis = false;
    //    agent.isStopped = false;

    //    return clone;
    //}
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = police2.GetMoveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = false;

        sprite = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        DetectPlayer();

        if (player != null)
        {
            Vector3 targetDistancePos = player.transform.position - transform.position;

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= police2.GetAttackRange)
            {
                // 사정거리 안에 들어오면 공격하고, 움직임 멈춤
                isInAttackRange = true;
                TryShoot();
                StopMovement();
            }
            else
            {
                // 사정거리 밖이면 움직임을 다시 시작
                isInAttackRange = false;
                SetAgentPosition();
            }

            if (targetDistancePos.x < 0)
            {
                sprite.flipX = false;
                gunSprite.flipY = true;
            }
            else
            {
                sprite.flipX = true;
                gunSprite.flipY = false;
            }
        }
    }

    void OnEnable()
    {
        // NavMeshAgent 재설정
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            agent = this.AddComponent<NavMeshAgent>();  // NavMeshAgent가 없으면 추가
        }

        // 기존 agent 설정 복사
        agent.speed = police2.GetMoveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = false;

        // NavMeshAgent 리셋
        agent.enabled = false;  // 먼저 비활성화
        agent.enabled = true;   // 다시 활성화하여 NavMesh와 재연결

        // 경로 재초기화
        agent.ResetPath();      // 경로 초기화
    }

    // CircleCast2D를 이용하여 플레이어 감지
    private void DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, police2.GetSightRange, Vector2.zero, 0, playerLayer);

        if (hit.collider != null)
        {
            player = hit.transform;  // 플레이어를 감지하면 저장

            //플레이어 따라 가기
            SetAgentPosition();
        }
        else
        {
            player = null;  // 플레이어를 감지하지 못하면 null
            Debug.Log("Player null");
        }
    }
    void SetAgentPosition()
    {
        if (!isDead)
        {
            agent.isStopped = false;

            agent.SetDestination(new Vector3(player.position.x, player.position.y,
                transform.position.z));
        }
    }

    // 움직임을 멈추는 함수
    private void StopMovement()
    {
        agent.isStopped = true;
        Debug.Log("Chasing Stopped");
    }

    // 사정거리에 들어왔을 때 플레이어를 향해 총을 발사
    private void TryShoot()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Shoot();
            lastAttackTime = Time.time;
        }
    }

    // 총알 발사 메서드
    private void Shoot()
    {
        if (firePoint != null && bulletPrefab != null)
        {
            // 총알을 생성하고 플레이어를 향해 발사
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();

            if (bulletComponent != null)
            {
                //bulletComponent.SetTarget(player.position);
            }

            Debug.Log("총을 발사했습니다!");
        }
    }

    // 감지 범위를 그려 디버깅
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, police2.GetSightRange);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (police2.GetHp() <= 0f)
            {
                //애니메이션
                animator.SetBool("Walk", false);
                animator.SetBool("Die", true);

                isDead = true;

                // 사망 애니메이션이 끝난 후 오브젝트를 제거
                StartCoroutine(DieAndDestroy());
            }
            else
            {
                police2.SetHp(police2.GetHp() - WeaponManager.instance.GetDamage());
            }
            Debug.Log("몬스터 체력 :: " + police2.GetHp());
        }
    }

    private IEnumerator DieAndDestroy()
    {
        //추적 멈춤
        agent.isStopped = true;

        // 사망 애니메이션이 재생되는 시간만큼 대기 (예: 2초)
        yield return new WaitForSeconds(2f);  // 애니메이션 길이에 맞게 조정

        // 오브젝트 삭제
        Destroy(gameObject);
    }
}
