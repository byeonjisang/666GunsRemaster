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

    //������ ������ ����� �ϱ� ����.
    public void DebugMonsterInfo()
    {
        Debug.Log("���� �̸� :: " + police1.GetMonsterName);
        Debug.Log("���� ü�� :: " + police1.GetHp);
        Debug.Log("���� ���ݷ� :: " + police1.GetDamage);
        Debug.Log("���� �þ� :: " + police1.GetSightRange);
        Debug.Log("���� �̵��ӵ� :: " + police1.GetMoveSpeed);
        Debug.Log("���� �����Ÿ� :: " + police1.GetAttackRange);
    }
}
