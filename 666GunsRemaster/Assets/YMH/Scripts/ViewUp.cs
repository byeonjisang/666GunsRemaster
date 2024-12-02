using System.Collections;
using UnityEngine;

public class ViewUp : Buff, IBuff
{
    public override void ApplyBuff(int index)
    {
        Debug.Log("시야 증가");
        base.ApplyBuff(index);
    }
}