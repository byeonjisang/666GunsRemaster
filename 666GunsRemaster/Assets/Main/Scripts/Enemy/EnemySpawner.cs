using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner: MonoBehaviour
{
    public Transform[] spawnPoint;
    public float spawnTime;

    float time;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
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
