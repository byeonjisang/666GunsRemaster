using UnityEngine;

namespace Enemy
{
    public abstract class AttackStrategy : ScriptableObject, IAttackStrategy
    {
        public abstract void Execute(Enemy enemy);
    }
}