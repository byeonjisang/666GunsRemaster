using UnityEngine;

namespace Character.Enemy
{
    public class MeleeEnemy : Enemy
    {
        public override void Attack()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, EnemyStat.AttackRange);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    Debug.Log("근접 공격");
                    // 나중에 공격 후 아직까지 플레이어가 앞에 있는지 체크해야함
                    //collider.GetComponent<Player.Player>().TakeDamage(EnemyStat.Attack);
                }
            }
        }
    }
}