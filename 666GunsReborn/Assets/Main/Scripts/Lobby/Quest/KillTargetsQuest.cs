using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "KillTargets_Quest", menuName = "Datas/KillTargets_Quest", order = 1)]
public class KillTargets_Quest : QuestBase
{
    [Header("처치해야 할 적의 ID")]
    public int enemyId;

    [Header("목표 처치 수")]
    public int targetKillCount;

    [Header("현재까지 처치한 수")]
    public int currentKillCount;

    public override string GetFormatText()
    {
        return $"적 처치: {currentKillCount}/{targetKillCount}";
    }

    public void AddKill()
    {
        if (isPartClear) return;
        currentKillCount++;

        if (currentKillCount >= targetKillCount) isPartClear = true;
    }
}
