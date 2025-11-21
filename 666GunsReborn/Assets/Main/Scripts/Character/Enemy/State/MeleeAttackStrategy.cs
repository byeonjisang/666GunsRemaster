using UnityEngine;

namespace Character.Enemy
{
    [CreateAssetMenu(fileName = "Melee Attack Strategy", menuName = "Enemy/AttackStrategy/Melee")]
    public class MeleeAttackStrategy : AttackStrategy
    {
        /// <summary>
        /// 공격 실행 메서드
        /// </summary>
        /// <param name="enemy"></param>
        public override void Execute(Enemy enemy)
        {
            Collider[] colliders = Physics.OverlapSphere(enemy.transform.position, enemy.EnemyStat.AttackRange);
            foreach (var collider in colliders)
            {
                /*
                if (collider.CompareTag("Player"))
                {
                    Debug.Log("근접 공격");
                    // 나중에 공격 후 아직까지 플레이어가 앞에 있는지 체크해야함
                    collider.GetComponent<Player>().Hit(enemy.EnemyStat.Atk);
                }
                */
            }
        }
    }    
}
