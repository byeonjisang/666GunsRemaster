using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    // 싱글톤 지속 여부 설정
    protected override bool IsPersistent => true;
    #region Fields and Properties
    // 스테이지 정보들
    [SerializeField]
    private List<StageData> stageDatas;
    // 현재 퀘스트 정보
    QuestData _currentQuest;

    // 현재 스테이지 번호
    private int _currentStageIndex;
    // 스테이지 클리어 했는지 기록
    private List<bool> _isStageClear;
    #endregion

    #region Start
    private void Start()
    {
        // 스테이지 클리어 상태 초기화
        _isStageClear = new List<bool>(new bool[stageDatas.Count]);
        for(int i = 0;i < _isStageClear.Count; i++)
            _isStageClear[i] = false;
    }
    #endregion

    #region Start Stage
    // 스테이지 시작
    public void StartStage(int stageIndex)
    {
        GameManager.Instance.ChangeGameMode(GameMode.INGAME);
        _currentStageIndex = stageIndex - 1;
        SceneManager.LoadScene("Stage " + stageIndex.ToString());
    }
    #endregion

    #region Stage Clear / Failed
    public void StageClear()
    {
        _isStageClear[_currentStageIndex] = true;
        UIManager.Instance.ShowStageClearUI(0f);
    }

    public void StageClear(float clearTime)
    {
        _isStageClear[_currentStageIndex] = true;
        UIManager.Instance.ShowStageClearUI(clearTime);
        GameManager.Instance._coin += Player.PlayerManager.Instance.GetHoldCoins();
        OffWeaponUIEvents();

        var currentQuest = QuestManager.Instance.currentQuest;
        if (currentQuest == null) return;

        // 현재 씬 이름을 가져와 비교
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        foreach (var quest in currentQuest.allQuests)
        {
            if (quest is StageClear_Quest stageQuest)
            {
                if (!stageQuest.isPartClear && stageQuest.stageName == currentSceneName)
                {
                    stageQuest.MarkStageClear(); // 퀘스트 파트 클리어 처리
                }
            }
        }

        // 전체 퀘스트 완료 여부 점검
        //QuestManager.Instance.CheckAllQuestClear();
    }

    public void StageFailed()
    {
        UIManager.Instance.ShowFailedUI();
        OffWeaponUIEvents();
    }
    #endregion

    private void OffWeaponUIEvents()
    {
        WeaponUIEvents.OnUpdateCooldownUI = null;
        WeaponUIEvents.OnUpdateWeaponImage = null;
        WeaponUIEvents.OnUpdateBulletUI = null;
        WeaponUIEvents.OnSwitchWeaponUI = null;
        WeaponUIEvents.OnUpdateReloadSlider = null;
    }

    #region Get Stage Clear State
    public List<bool> GetStageClearState()
    {
        return _isStageClear;
    }
    #endregion
}


// 퀘스트 관련 기능들이 포함되어 있어 임시로 여기에 둠
// 나중에 다시 적용시켜야 함
// public void DeadEnemy(GameObject enemyObject)
    // {

    //     stageController.DeadEnemy(enemyObject);

    //     currentQuest = QuestManager.Instance.currentQuest;

    //     if (currentQuest == null)
    //     {
    //         Debug.LogWarning("퀘스트 없음");
    //         return;
    //     }

    //     // 적 ID 가져오기
    //     var enemy = enemyObject.GetComponent<BaseEnemy>();
    //     if (enemy == null)
    //     {
    //         Debug.LogWarning("DeadEnemy: Enemy 컴포넌트가 없습니다.");
    //         return;
    //     }

    //     int killedEnemyId = enemy.id;

    //     foreach (var quest in currentQuest.allQuests)
    //     {
    //         if (quest is KillTargets_Quest killQuest)
    //         {
    //             if (!killQuest.isPartClear && killQuest.enemyId == killedEnemyId)
    //             {
    //                 killQuest.AddKill(); // 처치 수 증가
    //                 Debug.Log($"퀘스트 목표: ID {killedEnemyId} 적 처치 → {killQuest.currentKillCount}/{killQuest.targetKillCount}");
    //             }
    //         }
    //     }

    //     // 전체 퀘스트 완료 여부 확인
    //     QuestManager.Instance.CheckAllQuestClear();
    // }