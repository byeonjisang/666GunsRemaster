using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) // �ڱ� �ڽŰ� �浹 ����
        {
            //Destroy(gameObject);
        }
    }
}
