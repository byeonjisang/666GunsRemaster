using UnityEngine;

namespace Enemy
{
    public interface IAttackStrategy
    {
        public void Execute(Enemy enemy);
    }
}