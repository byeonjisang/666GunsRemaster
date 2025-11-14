using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundController : MonoBehaviour
{
    [System.Serializable]
    struct MonsterSpawnWave
    {
        public float SpawnTime;
        public GameObject MonsterPrefab;
        public Transform SpawnPoint;
    }

    [Header("Round Settings")]
    // 몬스터 소환 웨이브들
    [SerializeField] private MonsterSpawnWave[] monsterSpawnWaves;

    // 라운드 클리어 후 다음 라운드에 전달하기 위한 이벤트
    [Header("Complete Round Event")]
    [SerializeField] private UnityEvent onRoundComplete;

    [Header("Round Walls")]
    // 라운드 벽들
    [SerializeField] private GameObject[] roundWalls;

    // 라운드가 시작됐는지 여부
    private bool isRoundStated = false;
    // 살아있는 몬스터들
    private List<GameObject> activedMonsters = new List<GameObject>();

    private void Start()
    {
        // 라운드 벽 비활성화
        foreach (GameObject wall in roundWalls)
        {
            wall.SetActive(false);
        }
    }

    /// <summary>
    /// 라운드 시작
    /// </summary>
    public void StartRound()
    {
        // 이미 라운드 시작됐으면 무시
        if (isRoundStated)
            return;

        // 라운드 시작 체크
        isRoundStated = true;

        // 라운드 벽 활성화
        foreach (GameObject wall in roundWalls)
        {
            wall.SetActive(true);
        }

        // 몬스터 소환 등록
        foreach (MonsterSpawnWave wave in monsterSpawnWaves)
        {
            StartCoroutine(SpawnMonsterWave(wave));
        }
    }

    // 몬스터 소환 웨이브 등록
    private IEnumerator SpawnMonsterWave(MonsterSpawnWave wave)
    {
        // 대기 시간 후 몬스터 소환
        yield return new WaitForSeconds(wave.SpawnTime);

        // 몬스터 소환
        GameObject monsterPrefab = Instantiate(wave.MonsterPrefab, wave.SpawnPoint.position, wave.SpawnPoint.rotation);
        activedMonsters.Add(monsterPrefab);
    }

    // 몬스터들이 죽었는지 체크
    private void Update()
    {
        if (!isRoundStated)
            return;

        // 살아있는 몬스터들 체크
        foreach (GameObject Monster in activedMonsters)
        {
            if (Monster == null)
                activedMonsters.Remove(Monster);
        }

        // 살아있는 몬스터가 없으면 라운드 클리어
        if (activedMonsters.Count == 0)
        {
            isRoundStated = false;
            CompleteRound();
        }
    }
    
    // 라운드 클리어
    private void CompleteRound()
    {
        Debug.Log("라운드 클리어");

        // 라운드 벽 비활성화
        foreach (GameObject wall in roundWalls)
        {
            wall.SetActive(false);
        }
        // 라운드 클리어 이벤트 호출
        onRoundComplete.Invoke();
    }
}
