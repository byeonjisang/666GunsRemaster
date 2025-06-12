using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnWaveData", menuName = "Datas/SpawnWaveData", order = 4)]
public class SpawnWaveData : ScriptableObject
{
    //public List<MonsterSpawnWave> monsterSpawnWave;
    public List<RoundSpawnData> roundSpawnDatas;
}

[System.Serializable]
public class MonsterSpawnWave
{
    public GameObject monsterPrefab;
    public Vector3 spawnPosition;
    public float spawnTime;
}

[System.Serializable]
public class RoundSpawnData
{
    public int roundIndex;
    public List<MonsterSpawnWave> monsterSpanwWaves;
}