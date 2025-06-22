using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageClear_Quest", menuName = "Datas/StageClear_Quest", order = 1)]
public class StageClear_Quest : QuestBase
{
    [Header("클리어해야 할 스테이지 이름")]
    public string stageName;

    public override string GetFormatText()
    {
        return $"{stageName} 스테이지 클리어";
    }

    public void MarkStageClear()
    {
        isPartClear = true;
    }
}