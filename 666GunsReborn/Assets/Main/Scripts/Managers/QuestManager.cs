using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    protected override bool IsPersistent => true;

    public QuestData currentQuest;

    protected override void Awake() 
    {
        base.Awake();
    }

    //void Start()
    //{
    //    if (currentQuest == null)
    //    {
    //        currentQuest = .Load<QuestData>("Datas/KillTargets_Quest");
    //        if (currentQuest == null)
    //        {
    //            Debug.LogWarning("QuestManager: 기본 퀘스트를 로드하지 못했습니다.");
    //        }
    //        else
    //        {
    //            Debug.Log($"기본 퀘스트 로드 성공: {currentQuest.name}");
    //        }
    //    }
    //}

    public void OnEnemyKilled(int enemyId)
    {
        foreach (var quest in currentQuest.allQuests)
            if (quest is KillTargets_Quest kq && !kq.isPartClear && kq.enemyId == enemyId)
                kq.AddKill();
        CheckAllQuestClear();
    }

    public void OnStageCleared(string stageName)
    {
        foreach (var quest in currentQuest.allQuests)
            if (quest is StageClear_Quest sq && !sq.isPartClear && sq.stageName == stageName)
                sq.MarkStageClear();
        CheckAllQuestClear();
    }

    public void CheckAllQuestClear()
    {
        bool allClear = true;
        foreach (var q in currentQuest.allQuests)
            if (!q.isPartClear) allClear = false;

        if (allClear) currentQuest.questState = QuestState.CLEAR;
    }
}
