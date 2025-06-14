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
    }

    public void StageClear(float clearTime)
    {
        isStageClear[currentStageIndex] = true;
        UIManager.Instance.ShowStageClearUI(clearTime);
        //SceneManager.LoadScene("Stage Select");
    }

    public void StageFailed()
    {
        Debug.Log("Stage Failed");
        UIManager.Instance.ShowFailedUI();
    }

    public bool[] GetStageClearState()
    {
        return isStageClear;
    }
}
