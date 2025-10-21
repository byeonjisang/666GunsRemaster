using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    protected override bool IsPersistent => true;

    [SerializeField]
    private List<StageData> stageDatas;

    private StageController stageController;

    QuestData currentQuest;

    // 현재 스테이지 번호
    private int currentStageIndex;
    // 스테이지 클리어 했는지 기록
    private bool[] isStageClear = {false, false, false, false};

    protected override void Awake()
    {
        base.Awake();
        stageController = GetComponent<StageController>();
    }

    public void StartStage(int stageIndex)
    {
        GameManager.Instance.ChangeGameMode(GameMode.INGAME);
        currentStageIndex = stageIndex - 1;
        SceneManager.LoadScene("Stage " + stageIndex.ToString());
        stageController.StartStage(stageDatas[currentStageIndex]);
    }

    public void DeadEnemy(GameObject enemyObject)
    {

        stageController.DeadEnemy(enemyObject);

        currentQuest = QuestManager.Instance.currentQuest;

        if (currentQuest == null)
        {
            Debug.LogWarning("퀘스트 없음");
            return;
        }

        // 적 ID 가져오기
        var enemy = enemyObject.GetComponent<BaseEnemy>();
        if (enemy == null)
        {
            Debug.LogWarning("DeadEnemy: Enemy 컴포넌트가 없습니다.");
            return;
        }

        int killedEnemyId = enemy.id;

        foreach (var quest in currentQuest.allQuests)
        {
            if (quest is KillTargets_Quest killQuest)
            {
                if (!killQuest.isPartClear && killQuest.enemyId == killedEnemyId)
                {
                    killQuest.AddKill(); // 처치 수 증가
                    Debug.Log($"퀘스트 목표: ID {killedEnemyId} 적 처치 → {killQuest.currentKillCount}/{killQuest.targetKillCount}");
                }
            }
        }

        // 전체 퀘스트 완료 여부 확인
        QuestManager.Instance.CheckAllQuestClear();
    }

    public void StageClear(float clearTime)
    {
        isStageClear[currentStageIndex] = true;
        UIManager.Instance.ShowStageClearUI(clearTime);
        GameManager.Instance._coin += PlayerManager.Instance.GetHoldCoins();
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
        QuestManager.Instance.CheckAllQuestClear();
    }

    public void StageFailed()
    {
        Debug.Log("Stage Failed");
        UIManager.Instance.ShowFailedUI();
        OffWeaponUIEvents();
    }

    private void OffWeaponUIEvents()
    {
        WeaponUIEvents.OnUpdateCooldownUI = null;
        WeaponUIEvents.OnUpdateWeaponImage = null;
        WeaponUIEvents.OnUpdateBulletUI = null;
        WeaponUIEvents.OnSwitchWeaponUI = null;
        WeaponUIEvents.OnUpdateReloadSlider = null;
    }

    public bool[] GetStageClearState()
    {
        return isStageClear;
    }
}
