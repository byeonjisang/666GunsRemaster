using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner: MonoBehaviour
{
    public Transform[] spawnPoint;
    public float spawnTime;

    float time;

    public static EnemySpawner instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        spawnPoint = GetComponentsInChildren<Transform>();
        spawnTime = 3.5f;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > spawnTime)
        {
            time = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = EnemyObjectPool.instance.GetObject(Random.Range(0, 2));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    
    }
}
