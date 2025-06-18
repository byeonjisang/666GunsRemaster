using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class QuestBase
{
    [Header("�� ���� ǥ���� ���� id��ȣ")] public int formatId = -1;

    /// <summary>
    /// ����Ʈ �������� ��Ʈ �� �ϳ��� �� ����Ʈ�� Ŭ�����ߴ°�?
    /// </summary>
    [HideInInspector] public bool isPartClear = false;

    abstract public string GetFormatText();
}

[System.Serializable]
public class StageClear_Quest : QuestBase
{
    public bool _isStageClear = false;

    public override string GetFormatText()
    {
        throw new System.NotImplementedException();
    }


}

//����Ʈ Ŭ���� ����
[System.Serializable]
public enum QuestState
{
    NEVER_RECEIVED,

    ONGOING,

    CLEAR,

    CLEARED_PAST
}