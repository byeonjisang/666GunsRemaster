using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 퀘스트의 정보를 저장하는 클래스
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "Quest", menuName = "Add Quest/Quest")]
public class QuestData : ScriptableObject
{
    [HideInInspector] public QuestState questState = QuestState.NEVER_RECEIVED;

    [Header("고유한 퀘스트의 ID")]
    [SerializeField] public int questId;
    [Space(50)]

    ///퀘스트 추가 시 작성 예정
    [Header("퀘스트 목표")]
    //[SerializeField] public Quest_KillTargets[] killTargetsQuests; //적을 처치하는 목표
    //[SerializeField] public Quest_GetItems[] getItemsQuests; //아이템을 획득하는 목표

    [Space(50)]
    [Header("퀘스트의 목표 중 하나만 수행해도 되는 타입인가?")]
    [SerializeField] public bool isOptionalQuestType;

    [Space(10)]
    [Header("퀘스트를 완료 시 지급 경험치")]
    [SerializeField] public float expAmount;

    [Space(10)]
    [Header("퀘스트를 진행하는 씬 이름")] public string questScene;
    [Header("퀘스트를 의뢰한 대상의 위치")] public Vector3 sourcePos;
    [Header("퀘스트를 수행하러 가야 할 목적지 위치")] public Vector3 destinationPos;

    /// <summary>
    /// 해당 퀘스트가 가지는 모든 부분적 퀘스트
    /// </summary>
    public QuestBase[] allQuests;
}
