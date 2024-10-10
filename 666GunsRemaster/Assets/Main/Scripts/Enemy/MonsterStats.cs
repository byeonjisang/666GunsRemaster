using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public EnemyTest1 test1;
    public EnemyTest1 monsterData { set { monsterData = value; } }

    //몬스터의 정보를 디버그 하기 위함.
    public void DebugMonsterInfo()
    {
        Debug.Log("몬스터 이름 :: " + test1.GetMonsterName);
        Debug.Log("몬스터 체력 :: " + test1.GetHp);
        Debug.Log("몬스터 공격력 :: " + test1.GetDamage);
        Debug.Log("몬스터 시야 :: " + test1.GetSightRange);
        Debug.Log("몬스터 이동속도 :: " + test1.GetMoveSpeed);
    }
}
