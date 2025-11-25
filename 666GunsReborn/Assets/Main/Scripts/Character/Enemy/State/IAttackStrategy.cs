using UnityEngine;

namespace Character.Enemy
{
    public interface IAttackStrategy
    {
        public void Execute(Enemy enemy);
    }
}