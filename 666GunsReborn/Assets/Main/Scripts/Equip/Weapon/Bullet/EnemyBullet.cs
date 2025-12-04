using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Weapon.Bullet
{
    public class EnemyBullet : BulletBase
    {
        // 플레이어에게 데미지 부여
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Character.Player.Player>().TakeDamage(1);
                //Destroy(gameObject);
            }
        }
    }  
}