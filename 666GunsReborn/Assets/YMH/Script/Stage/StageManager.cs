using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    [Header("Portal")]
    [SerializeField]
    //private GameObject portal;

    private StageController stageController;

    // ���� �������� �ε���
    private int currentStageIndex;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        stageController = GetComponent<StageController>();
    }

    public void StartStage(int stageIndex)
    {
        currentStageIndex = stageIndex;
        SceneManager.LoadScene("Stage " + stageIndex.ToString());
        stageController.StartStage(currentStageIndex);
    }

    public void DeadEnemy(GameObject enemyObject) 
    {
        stageController.DeadEnemy(enemyObject);
    }
}
