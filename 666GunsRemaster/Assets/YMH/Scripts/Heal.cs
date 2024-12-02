using Character.Player;
using System.Collections;
using UnityEngine;

public class Heal : Buff, IBuff
{
    public override void ApplyBuff(int index)
    {
        Debug.Log("회복");
        PlayerController.Instance.Health += buffValue[index];
        UIManager.Instance.UpdatePlayerHealth(0, PlayerController.Instance.Health);

        base.ApplyBuff(index);
    }
}