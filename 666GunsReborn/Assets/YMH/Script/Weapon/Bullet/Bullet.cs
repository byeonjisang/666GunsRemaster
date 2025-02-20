using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigid;

    private float damage;
    private float speed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void SetSpeed(float damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }

    private void FixedUpdate()
    {
        rigid.velocity = transform.forward * speed;
    }
}