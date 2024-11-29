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
    public static BuffManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField]
    private GameObject BuffObject;
    private List<IBuff> buffList = new List<IBuff>();
    private IBuff[] buffsToSelect = new IBuff[2];

    private void Start()
    {
        buffList.Add(gameObject.AddComponent<Heal>());
        buffList.Add(gameObject.AddComponent<SpeedUp>());
        //buffList.Add(new BulletUp());
        //buffList.Add(new ViewUp());
        //buffList.Add(new DashCountUp());
    }

    //버프 선택
    public void SelectBuff()
    {
        Time.timeScale = 0;
        BuffObject.SetActive(true);
        SettingSelect();
    }

    private void SettingSelect()
    {
        int[] selectBuffNum = new int[2];
        selectBuffNum[0] = Random.Range(0, buffList.Count);
        Debug.Log(selectBuffNum[0]);
        while (true)
        {
            selectBuffNum[1] = Random.Range(0, buffList.Count);
            if (selectBuffNum[0] != selectBuffNum[1])
                break;

            Debug.Log(selectBuffNum[1]);
        }

        for(int index = 0; index < buffsToSelect.Length; index++)
        {
            buffsToSelect[index] = buffList[selectBuffNum[index]];
            buffsToSelect[index].ShowBuff(index);

            UIManager.Instance.OnButtonBuff(index, () => buffsToSelect[index].ApplyBuff());
        }
    }
}