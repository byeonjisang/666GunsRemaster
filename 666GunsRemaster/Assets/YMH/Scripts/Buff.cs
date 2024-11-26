using System.Collections;
using UnityEngine;

public class Buff : MonoBehaviour
{
    protected BuffData buffData;
    protected string buffName;
    protected string BuffContent;

    protected void BuffInit()
    {
        buffName = buffData.BuffName;
        BuffContent = buffData.BuffContent;
    }


}