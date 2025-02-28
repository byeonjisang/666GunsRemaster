using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int amount;

    public void SetAmount(int amount)
    {
        this.amount = amount;
    }

    public int GetAmount()
    {
        return amount;
    }
}
