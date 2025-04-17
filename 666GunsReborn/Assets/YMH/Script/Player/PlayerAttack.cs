using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;

    public bool IsAttacking { get; private set; } = false;

    public PlayerAttack(Player player)
    {
        this.player = player;
    }

    public void StartAttack()
    {
        if (IsAttacking)
            return;

        IsAttacking = true;
    }

    public void StopAttack()
    {
        IsAttacking = false;
    }
}