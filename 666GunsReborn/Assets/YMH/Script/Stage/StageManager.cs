using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [Header("Portal")]
    [SerializeField]
    private GameObject portal;

    [Header("Enemy")]
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private List<Transform> enemySpawnTransforms;

    // 현재 스테이지 인덱스
    private int currentStageIndex = 0;

    // 살아있는 적들
    private List<GameObject> aliveEnemies = new List<GameObject>();

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        foreach(Transform enemySpawnTransform in enemySpawnTransforms)
        {
            Instantiate(enemyPrefab, enemySpawnTransform.position, enemySpawnTransform.rotation);
        }
    }
}
