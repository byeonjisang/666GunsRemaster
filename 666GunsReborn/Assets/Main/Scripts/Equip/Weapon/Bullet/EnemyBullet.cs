using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �ڱ� �ڽŰ� �浹 ����
        {
            other.GetComponent<Player>().Hit(1);
            //Destroy(gameObject);
        }
    }
}
