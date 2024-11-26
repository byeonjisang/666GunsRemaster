using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BuffType
{
    Heal = 0,
    SpeedUp,
    BulletUp,
    VeiwUp,
    DashCountUp,
}

public class BuffManager : MonoBehaviour
{
    private List<IBuff> buffList = new List<IBuff>();
    private IBuff[] BuffsToSelect = new IBuff[2];

    private void SettingSelect()
    {
        int[] selectBuffNum = new int[2];
        selectBuffNum[0] = Random.Range(0, buffList.Count);
        while (true)
        {
            selectBuffNum[1] = Random.Range(0, buffList.Count);
            if (selectBuffNum[0] != selectBuffNum[1])
                break;
        }


    }
}
