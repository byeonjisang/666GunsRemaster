<<<<<<< HEAD
<<<<<<<< HEAD:666GunsReborn/Assets/Main/Scripts/Character/Enemy/Old Script/Enemy/AttackStrategy.cs
using UnityEngine;

namespace Enemy
{
    public abstract class AttackStrategy : ScriptableObject, IAttackStrategy
    {
        public abstract void Execute(Enemy enemy);
    }
========
using UnityEngine;

namespace Character.Enemy
{
    public abstract class AttackStrategy : ScriptableObject, IAttackStrategy
    {
        public abstract void Execute(Enemy enemy);
    }
>>>>>>>> origin/main:666GunsReborn/Assets/Main/Scripts/Character/Enemy/State/AttackStrategy.cs
=======
using UnityEngine;

namespace Character.Enemy
{
    public abstract class AttackStrategy : ScriptableObject, IAttackStrategy
    {
        public abstract void Execute(Enemy enemy);
    }
>>>>>>> origin/main
}