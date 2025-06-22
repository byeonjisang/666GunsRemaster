using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// 퀘스트의 정보를 저장하는 클래스
/// </summary>
[CreateAssetMenu(fileName = "QuestData", menuName = "Datas/QuestData", order = 1)]
public class QuestData : ScriptableObject
{
    public int questId;
    public bool isOptionalQuestType;

    /// 이 퀘스트가 수행되는 씬의 이름 (예: "Stage1", "BossRoom")
    public string questScene;

    /// <summary>
    /// 이 퀘스트가 요구하는 세부 목표 목록
    /// 각각의 목표는 QuestBase를 상속한 개별 퀘스트 파트 (예: KillTargets_Quest, StageClear_Quest 등)로 구성됨
    /// </summary>
    public QuestBase[] allQuests;

    /// 퀘스트의 현재 진행 상태 (수락 전 / 진행 중 / 완료 등)
    public QuestState questState = QuestState.NEVER_RECEIVED;
}
