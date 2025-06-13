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
