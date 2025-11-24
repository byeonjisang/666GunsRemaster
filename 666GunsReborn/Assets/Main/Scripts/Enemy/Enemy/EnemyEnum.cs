using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    // 적의 공격 타입
    public enum AttackType
    {
        Melee,
        Range,
    }

    public enum EnemyState
    {
        Chase,
        Attack,
        Die,
    }
}

