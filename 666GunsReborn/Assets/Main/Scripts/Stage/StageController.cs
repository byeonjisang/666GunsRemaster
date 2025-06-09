using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private StageData currentStageData;

    // 살아있는 적들
    private List<GameObject> aliveEnemies = new List<GameObject>();

    private float stageTimer = 0;
    private bool isStageStart = false;
    private bool isStageClear = false;
    private bool isSpawning = false;

    private void Update()
    {
        if(isStageStart){
            stageTimer -= Time.deltaTime;
            UIManager.Instance.UpdateTimer(stageTimer);
            if (stageTimer <= 0)
            {
                isStageStart = false;
                isSpawning = false;
                StageManager.Instance.StageFailed();
            }
        }
    }

    public void StartStage(StageData currentStageData)
    {
        isStageStart = true;
        this.currentStageData = currentStageData;
        stageTimer = this.currentStageData.stageTime;
        StartCoroutine(SpawnMonsterRoutine(this.currentStageData.spawnWaveData));
    }

    private IEnumerator SpawnMonsterRoutine(SpawnWaveData spawnWaveData)
    {
        // 스폰 순서를 보장하기 위해 spawnTime 기준으로 정렬
        spawnWaveData.monsterSpawnWave.Sort((a, b) => a.spawnTime.CompareTo(b.spawnTime));

        float elapsedTime = 0f;
        isSpawning = true;

        // 스폰 시작
        foreach (var wave in spawnWaveData.monsterSpawnWave)
        {
            float waitTime = wave.spawnTime - elapsedTime;
            if (waitTime > 0f)
                yield return new WaitForSeconds(waitTime);

            //임시 소환(오브젝트풀로 구현해야 함)
            aliveEnemies.Add(Instantiate(wave.monsterPrefab, wave.spawnPosition, Quaternion.identity));

            elapsedTime = wave.spawnTime;
        }

        // 스폰 끝남
        isSpawning = false;
    }

    // 적이 죽었을 때 호출되는 메서드
    public void DeadEnemy(GameObject enemyObject)
    {
        aliveEnemies.Remove(enemyObject);

        //임시 삭제(나중에 오브젝트풀로 구현)
        Destroy(enemyObject);

        // 모든 적이 죽었는지 확인 후 스테이지 클리어 체크
        if (!isSpawning && aliveEnemies.Count <= 0)
        {
            Debug.Log("스테이지 클리어");
            isStageStart = false;
            isStageClear = true;
            StageManager.Instance.StageClear(currentStageData.stageTime - stageTimer);
        }
    }
}