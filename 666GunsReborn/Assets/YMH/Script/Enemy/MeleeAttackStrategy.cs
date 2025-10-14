using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Melee Attack Strategy", menuName = "Enemy/AttackStrategy/Melee")]
    public class MeleeAttackStrategy : AttackStrategy
    {
        public override void Execute(Enemy enemy)
        {
            Collider[] colliders = Physics.OverlapSphere(enemy.transform.position, enemy.EnemyStat.AtkRange);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    Debug.Log("근접 공격");
                }
            }
        }
    }    
}
