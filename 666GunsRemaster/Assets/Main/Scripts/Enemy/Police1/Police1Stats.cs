using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police1Stats : MonoBehaviour
{
    public Police1 police1;
    public void SetData(Police1 data)
    {
        police1 = data;
    }

    //몬스터의 정보를 디버그 하기 위함.
    public void DebugMonsterInfo()
    {
        Debug.Log("몬스터 이름 :: " + police1.GetMonsterName);
        Debug.Log("몬스터 체력 :: " + police1.GetHp);
        Debug.Log("몬스터 공격력 :: " + police1.GetDamage);
        Debug.Log("몬스터 시야 :: " + police1.GetSightRange);
        Debug.Log("몬스터 이동속도 :: " + police1.GetMoveSpeed);
        Debug.Log("몬스터 사정거리 :: " + police1.GetAttackRange);
    }
}
