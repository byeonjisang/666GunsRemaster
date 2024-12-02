using System.Collections;
using UnityEngine;

public class BulletUp : Buff, IBuff
{
    public override void ApplyBuff(int index)
    {
        Debug.Log("총알 증가");
        Gun.WeaponManager.instance.BulletCountUp((int)buffValue[index]);

        base.ApplyBuff(index);
    }
}