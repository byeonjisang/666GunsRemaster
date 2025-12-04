using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Weapon.Bullet
{
    public class Bullet : BulletBase
    {
        private void OnTriggerEnter(Collider other)
        {
            switch(GameManager.Instance._gameMode){
                case GameMode.WEAPONTEST:
                    OnCollisionBulletInWeaponTest(other);
                    break;
                case GameMode.INGAME:
                    
                    break;
                default:
                    OnCollisionBulletInInGame(other);
                    break;
            }
        }

        private void OnCollisionBulletInWeaponTest(Collider other){

        }

        private void OnCollisionBulletInInGame(Collider other){
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Bullet hit enemy: " + other.name);

                Character.Enemy.Enemy enemyScript = other.GetComponent<Character.Enemy.Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage((int)power);
                }
                ReturnToPool();
            }
        }
    }   
}