using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public EnemyTest1 test1;
    public EnemyTest1 monsterData { set { monsterData = value; } }

    //������ ������ ����� �ϱ� ����.
    public void DebugMonsterInfo()
    {
        Debug.Log("���� �̸� :: " + test1.GetMonsterName);
        Debug.Log("���� ü�� :: " + test1.GetHp);
        Debug.Log("���� ���ݷ� :: " + test1.GetDamage);
        Debug.Log("���� �þ� :: " + test1.GetSightRange);
        Debug.Log("���� �̵��ӵ� :: " + test1.GetMoveSpeed);
    }
}
