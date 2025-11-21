using UnityEngine;

namespace Character.Enemy
{
    public class EnemyStateContext : CharacterStateContext
    {
        public ChaseState ChaseState;
        public AttackState AttackState;
        public DeadState DeadState;

        public EnemyStateContext(Enemy enemy)
        {
            ChaseState = new ChaseState(enemy);
            AttackState = new AttackState(enemy);
            DeadState = new DeadState(enemy);
        }
    }
}