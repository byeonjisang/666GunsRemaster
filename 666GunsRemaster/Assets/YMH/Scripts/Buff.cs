using System.Collections;
using UnityEngine;

public class Buff : MonoBehaviour, IBuff
{
    protected BuffData buffData;
    protected Sprite buffIcon;
    protected string buffName;
    protected string BuffContent;

    protected virtual void Start()
    {
        buffData = Resources.Load("Datas/BuffData/" + this.GetType().Name) as BuffData;
        BuffInit();
    }

    protected void BuffInit()
    {
        Debug.Log("BuffInit");
        buffIcon = buffData.BuffImage;
        buffName = buffData.BuffName;
        BuffContent = buffData.BuffContent;
    }

    public virtual void ShowBuff(int index)
    {
        Debug.Log("Show Buff");
        Debug.Log(buffName);
        UIManager.Instance.ShowBuff(index, buffIcon, buffName, BuffContent);
    }

    public virtual void ApplyBuff()
    {

    }
}