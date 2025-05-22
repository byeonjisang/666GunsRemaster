using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageType
{
    Tutorial,
    Normal,
    Boss,
}

[CreateAssetMenu(fileName = "StageData", menuName = "Datas/StageData", order = 3)]
public class StageData : ScriptableObject
{
    public int stageIndex; // 스테이지 인덱스
    public string stageName; // 스테이지 이름
    public StageType stageType; // 스테이지 타입
    public float stageTime; // 스테이지 시간
    public SpawnWaveData spawnWaveData;
}