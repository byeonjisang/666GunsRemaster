using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    protected BuffData buffData;
    protected Sprite buffIcon;
    public string buffName;
    protected string buffContent;
    protected List<float> buffValue;

    protected virtual void Start()
    {
        buffData = Resources.Load("Datas/BuffData/" + this.GetType().Name) as BuffData;
        BuffInit();
    }

    protected void BuffInit()
    {
        buffIcon = buffData.BuffImage;
        buffName = buffData.BuffName;
        buffContent = buffData.BuffContent;
        buffValue = buffData.BuffValue;
    }

    public virtual void ShowBuff(int index)
    {
        Debug.Log("Show Buff");
        Debug.Log(buffName);
        UIManager.Instance.ShowBuff(index, buffIcon, buffName, buffContent);
    }

    public virtual void ApplyBuff(int index)
    {
        Debug.Log("Apply Buff");
        Time.timeScale = 1;
        UIManager.Instance.BuffWindowOnOff(false);
        EnemySpawner.instance.spawnTime -= 0.25f;
    }
}