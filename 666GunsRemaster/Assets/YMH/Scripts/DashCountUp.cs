using System.Collections;
using UnityEngine;

public class DashCountUp : Buff, IBuff
{
    public override void ApplyBuff(int index)
    {
        Debug.Log("대쉬 증가");
        Character.Player.PlayerController.Instance.DashCount += (int)buffValue[index];
        Character.Player.PlayerController.Instance.CurrentDashCount += (int)buffValue[index];
        base.ApplyBuff(index);
    }
}