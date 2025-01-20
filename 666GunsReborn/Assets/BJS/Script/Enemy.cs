using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//��� ������ �⺻���� ������ �� Ŭ����
public class Enemy : MonoBehaviour
{
    //ü�� ���� ����
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

    //�ǰ� �� �ൿ
    IEnumerator OnDamage()
    {
        material.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        material.color = Color.white;
    }
}
