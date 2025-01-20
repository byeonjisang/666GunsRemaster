using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//모든 몬스터의 기본적인 로직이 될 클래스
public class Enemy : MonoBehaviour
{
    //체력 관련 변수
    public int maxHealth;
    public int currentHealth;

    bool _isChase = true;

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
        
    }
    void FixedUpdate()
    {
        if (_isChase && agent.enabled)
        {
            agent.SetDestination(target.position);
            FreezeVelo();
            animator.SetBool("isChase", true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            StartCoroutine(OnDamage());
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
}
