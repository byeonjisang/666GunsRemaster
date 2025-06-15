using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class QuestBase
{
    [Header("이 값을 표현할 포맷 id번호")] public int formatId = -1;

    /// <summary>
    /// 퀘스트 데이터의 파트 중 하나인 이 퀘스트를 클리어했는가?
    /// </summary>
    [HideInInspector] public bool isPartClear = false;

    abstract public string GetFormatText();
}
