using Character.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

enum BuffType
{
    Heal = 0,
    SpeedUp,
    BulletUp,
    ViewUp,
    DashCountUp,
}

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField]
    private List<IBuff> buffList = new List<IBuff>();
    private IBuff[] buffsToSelect = new IBuff[2];

    private void Start()
    {
        buffList.Add(gameObject.AddComponent<Heal>());
        buffList.Add(gameObject.AddComponent<SpeedUp>());
        buffList.Add(gameObject.AddComponent<BulletUp>());
        //buffList.Add(gameObject.AddComponent<ViewUp>());
        buffList.Add(gameObject.AddComponent<DashCountUp>());
    }

    //버프 선택
    public void SelectBuff()
    {
        Time.timeScale = 0;
        UIManager.Instance.BuffWindowOnOff(true);
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

        for (int index = 0; index < buffsToSelect.Length; index++)
        {
            buffsToSelect[index] = buffList[selectBuffNum[index]];
            buffsToSelect[index].ShowBuff(index);

            string buffType = buffsToSelect[index].GetType().Name;
            UIManager.Instance.OnButtonBuff(index, (buffType) => PlayerController.Instance.ApplyBuff(buffType), buffType);
        }
    }

    public IBuff CreateBuff(string buffType)
    {
        try
        {
            return buffList.FirstOrDefault(buff => buff.GetType().Name == buffType);
        }
        catch
        {
            throw new System.ArgumentException("Unkown buff type : " + buffType);
        }
    }
}