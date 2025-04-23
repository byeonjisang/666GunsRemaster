using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [Header("Portal")]
    [SerializeField]
    private GameObject portal;

    private StageController stageController;

    // ���� �������� �ε���
    private int currentStageIndex = 1;

    protected override void Awake()
    {
        base.Awake();
        stageController = GetComponent<StageController>();
    }

    private void Start()
    {
        StartStage();
    }

    public void StartStage()
    {
        stageController.StartStage(currentStageIndex);
    }

    public void DeadEnemy(GameObject enemyObject) 
    {
        stageController.DeadEnemy(enemyObject);
    }
}
