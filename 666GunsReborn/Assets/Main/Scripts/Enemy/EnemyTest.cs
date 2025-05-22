using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    private Color color;
    private Material enemyMaterial;
    private int health = 1;

    private void Awake()
    {
        enemyMaterial = GetComponent<Renderer>().material;
        color = enemyMaterial.color;
    }

    public void OnDamge(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // 적이 죽었을 때의 처리
            StageManager.Instance.DeadEnemy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void CheckedFromPlayer(bool isCheck)
    {
        if (isCheck)
        {
            enemyMaterial.color = Color.red;
        }
        else
        {
            enemyMaterial.color = color;
        }
    }
}
