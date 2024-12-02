using Character.Player;
using System.Collections;
using UnityEngine;

public class SpeedUp : Buff, IBuff
{
    public override void ApplyBuff(int index)
    {
        Debug.Log("스피드 증가");
        PlayerController.Instance.MoveSpeed += buffValue[index];

        base.ApplyBuff(index);
    }
}