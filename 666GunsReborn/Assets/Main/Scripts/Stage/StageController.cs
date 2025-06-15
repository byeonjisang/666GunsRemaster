using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private StageData currentStageData;

    // 살아있는 적들
    private List<GameObject> roundAliveEnemies = new List<GameObject>();

    // 라운드 입장 센서들
    public RoundEnterSenser[] roundEnterSensors;

    private int maxRound;
    public int currentRound = 1;
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
        // 변수 초기화
        currentRound = 1;
        stageTimer = 0;
        isStageStart = true;
        isStageClear = false;

        // 스테이지 데이터 설정
        this.currentStageData = currentStageData;
        this.maxRound = currentStageData.spawnWaveData.roundSpawnDatas.Count;

        // UI 초기화

        // 라운드 입장 센서 초기화


        stageTimer = this.currentStageData.stageTime;
    }

    private IEnumerator SpawnMonsterRoutine(List<MonsterSpawnWave> spawnWaveData)
    {
        // 스폰 순서를 보장하기 위해 spawnTime 기준으로 정렬
        spawnWaveData.Sort((a, b) => a.spawnTime.CompareTo(b.spawnTime));

        float elapsedTime = 0f;
        isSpawning = true;

        // 스폰 시작
        foreach (var wave in spawnWaveData)
        {
            float waitTime = wave.spawnTime - elapsedTime;
            if (waitTime > 0f)
                yield return new WaitForSeconds(waitTime);

            //임시 소환(오브젝트풀로 구현해야 함)
            roundAliveEnemies.Add(Instantiate(wave.monsterPrefab, wave.spawnPosition, Quaternion.identity));

            elapsedTime = wave.spawnTime;
        }

        // 스폰 끝남
        isSpawning = false;
    }

    private void RoundClear()
    {
        roundEnterSensors[currentRound - 1].SetRoundAbarsOn(false);
        currentRound += 1;
        if (currentRound > maxRound)
        {
            Debug.Log("스테이지 클리어");
            StageClaer();
        }
        else
        {
            Debug.Log("라운드 클리어, 다음 라운드 시작");
        }
    }

    public void EnterRound(int roundIndex)
    {
        if (roundIndex != currentRound)
        {
            Debug.LogWarning("잘못된 라운드에 입장");
            return;
        }

        Debug.Log("라운드 " + roundIndex + " 입장");

        // 플레이어를 라운드 맵에 가둠
        roundEnterSensors = FindObjectsOfType<RoundEnterSenser>();

        // 몬스터 소환 시작
        List<MonsterSpawnWave> roundWaveData = currentStageData.spawnWaveData.roundSpawnDatas.Find(x => x.roundIndex == roundIndex).monsterSpanwWaves;
        StartCoroutine(SpawnMonsterRoutine(roundWaveData));
    }

    private void StageClaer()
    {
        isStageStart = false;
        isStageClear = true;
        StageManager.Instance.StageClear(currentStageData.stageTime - stageTimer);
    }

    // 적이 죽었을 때 호출되는 메서드
    public void DeadEnemy(GameObject enemyObject)
    {
        roundAliveEnemies.Remove(enemyObject);

        //임시 삭제(나중에 오브젝트풀로 구현)
        Destroy(enemyObject);

        // 모든 적이 죽었는지 확인 후 스테이지 클리어 체크
        if (!isSpawning && roundAliveEnemies.Count <= 0)
        {
            Debug.Log("라운드 클리어");
            RoundClear();
        }
    }
}