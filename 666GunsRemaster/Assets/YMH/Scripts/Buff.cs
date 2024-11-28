using System.Collections;
using UnityEngine;

public class Buff : MonoBehaviour, IBuff
{
    protected BuffData buffData;
    protected Sprite buffIcon;
    protected string buffName;
    protected string BuffContent;

    protected void BuffInit()
    {
        buffIcon = buffData.BuffImage;
        buffName = buffData.BuffName;
        BuffContent = buffData.BuffContent;
    }

    public virtual void ShowBuff(int index)
    {
        UIManager.Instance.ShowBuff(index, buffIcon, buffName, BuffContent);
    }

    public virtual void ApplyBuff()
    {

    }
}