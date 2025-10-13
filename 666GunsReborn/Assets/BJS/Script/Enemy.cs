using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//��� ������ �⺻���� ������ �� Ŭ����
public class Enemys : MonoBehaviour
{
    //������ ������ ������ enum
    public enum EnemyType
    {
        A, B, C
    };

    public EnemyType enemyType;

    //ü�� ���� ����
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

    //�ǰ� �� �ൿ
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
