using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
        return $"��(ID:{enemyId}) óġ: {currentKillCount}/{targetKillCount}";
    }

    public void AddKill()
    {
        if (isPartClear) return;

        currentKillCount++;
        if (currentKillCount >= targetKillCount)
        {
            isPartClear = true;
        }
    }
}
