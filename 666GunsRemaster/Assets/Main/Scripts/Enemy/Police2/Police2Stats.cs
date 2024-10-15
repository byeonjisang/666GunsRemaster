using Gun.Bullet;
using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("몬스터 이름 :: " + police2.GetMonsterName);
        Debug.Log("몬스터 체력 :: " + police2.GetHp);
        Debug.Log("몬스터 공격력 :: " + police2.GetDamage);
        Debug.Log("몬스터 시야 :: " + police2.GetSightRange);
        Debug.Log("몬스터 이동속도 :: " + police2.GetMoveSpeed);
        Debug.Log("몬스터 사정거리 :: " + police2.GetAttackRange);
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = police2.GetMoveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        DetectPlayer();
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // 사정거리 내에 들어왔을 경우 공격
            if (distanceToPlayer <= police2.GetAttackRange)
            {
                TryShoot();
            }
        }
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
        }
    }
    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(player.position.x, player.position.y,
            transform.position.z));
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
}
