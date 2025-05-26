using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) // 자기 자신과 충돌 무시
        {
            //Destroy(gameObject);
        }
    }
}
