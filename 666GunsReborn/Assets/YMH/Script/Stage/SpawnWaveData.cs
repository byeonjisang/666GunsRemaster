using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnWaveData", menuName = "Datas/SpawnWaveData", order = 4)]
public class SpawnWaveData : ScriptableObject
{
    public List<MonsterSpawnWave> monsterSpawnWave;
}

[System.Serializable]
public class MonsterSpawnWave
{
    public GameObject monsterPrefab;
    public Vector3 spawnPosition;
    public float spawnTime;
}