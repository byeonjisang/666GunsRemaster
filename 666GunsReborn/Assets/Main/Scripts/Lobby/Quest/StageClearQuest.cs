using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

}
