using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police1Stats : MonoBehaviour
{
    public Police1 police1;
    public LayerMask playerLayer;    // 플레이어가 속한 레이어

    private Transform player;

    [SerializeField]
    private bool isInAttackRange = false;

    //길찾기 적용
    NavMeshAgent agent;
    public void SetData(Police1 data)
    {
        police1 = data;
    }

    //몬스터의 정보를 디버그 하기 위함.
    public void DebugMonsterInfo()
    {
        Debug.Log("몬스터 이름 :: " + police1.GetMonsterName);
        Debug.Log("몬스터 체력 :: " + police1.GetHp);
        Debug.Log("몬스터 공격력 :: " + police1.GetDamage);
        Debug.Log("몬스터 시야 :: " + police1.GetSightRange);
        Debug.Log("몬스터 이동속도 :: " + police1.GetMoveSpeed);
        Debug.Log("몬스터 사정거리 :: " + police1.GetAttackRange);
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = police1.GetMoveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        DetectPlayer();

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= police1.GetAttackRange)
            {
                // 사정거리 안에 들어오면 공격하고, 움직임 멈춤
                isInAttackRange = true;
                
            }
            else
            {
                // 사정거리 밖이면 움직임을 다시 시작
                isInAttackRange = false;
            }

            // 사정거리 밖이면 몬스터가 플레이어를 추적
            if (!isInAttackRange)
            {
                SetAgentPosition();
            }
            else
            {
                StopMovement();  // 사정거리 안에 있으면 움직임 멈추기
            }
        }
    }

    // CircleCast2D를 이용하여 플레이어 감지
    private void DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, police1.GetSightRange, Vector2.zero, 0, playerLayer);

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
        agent.isStopped = false;

        agent.SetDestination(new Vector3(player.position.x, player.position.y,
            transform.position.z));
    }

    // 움직임을 멈추는 함수
    private void StopMovement()
    {
        agent.isStopped = true;
    }
}
