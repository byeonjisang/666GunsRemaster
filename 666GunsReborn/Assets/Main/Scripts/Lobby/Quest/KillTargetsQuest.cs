using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "KillTargets_Quest", menuName = "Datas/KillTargets_Quest", order = 1)]
public class KillTargets_Quest : QuestBase
{
    [Header("óġ�ؾ� �� ���� ID")]
    public int enemyId;

    [Header("��ǥ óġ ��")]
    public int targetKillCount;

    [Header("������� óġ�� ��")]
    public int currentKillCount;

    public override string GetFormatText()
    {
        return $"�� óġ: {currentKillCount}/{targetKillCount}";
    }

    public void AddKill()
    {
        if (isPartClear) return;
        currentKillCount++;

        if (currentKillCount >= targetKillCount) isPartClear = true;
    }
}
