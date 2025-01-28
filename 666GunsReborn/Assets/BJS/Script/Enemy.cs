using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//모든 몬스터의 기본적인 로직이 될 클래스
public class Enemy : MonoBehaviour
{
    //몬스터의 종류를 구분할 enum
    public enum EnemyType
    {
        A, B, C
    };

    public EnemyType enemyType;

    //체력 관련 변수
    public int maxHealth;
    public int currentHealth;

    bool _isChase = true;
    bool _isAttack = false;

    public Transform target;
    

    [SerializeField]
    private NavMeshAgent agent;
    private Rigidbody rigid;
    private Material material;
    private Animator animator;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        material = GetComponent<Material>();
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        Targeting();
    }
    void FixedUpdate()
    {
        Chase();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            StartCoroutine(OnDamage());
        }
    }

    void Targeting()
    {
        float rad = 0f;
        float attackRange = 0f;

        RaycastHit[] hit = Physics.SphereCastAll(transform.position, rad, transform.forward, attackRange, LayerMask.GetMask("Player"));

        if (hit.Length > 0 && _isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    void Chase()
    {
        if (_isChase && agent.enabled)
        {
            agent.SetDestination(target.position);
            FreezeVelo();
            animator.SetBool("isChase", true);
        }
    }

    void FreezeVelo()
    {
        rigid.angularVelocity = Vector3.zero;
        rigid.velocity = Vector3.zero;
    }

    //피격 시 행동
    IEnumerator OnDamage()
    {
        material.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        material.color = Color.white;
    }

    IEnumerator Attack()
    {
        _isChase = false;
        _isAttack = true;

        yield return null;
    }
}
