using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    private Material enemyMaterial;

    private void Awake()
    {
        enemyMaterial = GetComponent<Renderer>().material;
    }

    public void CheckedFromPlayer(bool isCheck)
    {
        if (isCheck)
        {
            enemyMaterial.color = Color.red;
        }
        else
        {
            enemyMaterial.color = Color.white;
        }
    }
}
