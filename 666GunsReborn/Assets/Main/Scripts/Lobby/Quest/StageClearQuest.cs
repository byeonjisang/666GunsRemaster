using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageClear_Quest", menuName = "Datas/StageClear_Quest", order = 1)]
public class StageClear_Quest : QuestBase
{
    [Header("Ŭ�����ؾ� �� �������� �̸�")]
    public string stageName;

    public override string GetFormatText()
    {
        return $"{stageName} �������� Ŭ����";
    }

    public void MarkStageClear()
    {
        isPartClear = true;
    }
}