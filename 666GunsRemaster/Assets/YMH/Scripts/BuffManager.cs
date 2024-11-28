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
    [SerializeField]
    private GameObject BuffObject;
    private List<IBuff> buffList = new List<IBuff>();
    private IBuff[] buffsToSelect = new IBuff[2];

    private void Start()
    {
        buffList.Add(new Heal());
        //buffList.Add(new SpeedUp());
        //buffList.Add(new BulletUp());
        //buffList.Add(new ViewUp());
        //buffList.Add(new DashCountUp());
    }

    public void SelectBuff()
    {
        BuffObject.SetActive(true);
        SettingSelect();
    }

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

        for(int index = 0; index < buffsToSelect.Length; index++)
        {
            buffsToSelect[index] = buffList[selectBuffNum[index]];
            buffsToSelect[index].ShowBuff(index);
        }
    }
}
