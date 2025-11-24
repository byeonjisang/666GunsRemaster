<<<<<<< HEAD
<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundController : MonoBehaviour
{
    [System.Serializable]
    struct EnemySpawnWave
    {
        public float SpawnTime;
        public GameObject EnemyPrefab;
        public Transform SpawnPoint;
    }

    [Header("Round Settings")]
    // 몬스터 소환 웨이브들
    [SerializeField] private EnemySpawnWave[] EnemySpawnWaves;

    // 라운드 클리어 후 다음 라운드에 전달하기 위한 이벤트
    [Header("Complete Round Event")]
    [SerializeField] private UnityEvent onRoundComplete;

    [Header("Round Walls")]
    // 라운드 벽들
    [SerializeField] private GameObject[] roundWalls;

    // 라운드가 시작됐는지 여부
    private bool isRoundStated = false;
    // 살아있는 몬스터들
    private List<GameObject> activedEnemies = new List<GameObject>();

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
        foreach (EnemySpawnWave wave in EnemySpawnWaves)
        {
            StartCoroutine(SpawnEnemyWave(wave));
        }
    }

    // 몬스터 소환 웨이브 등록
    private IEnumerator SpawnEnemyWave(EnemySpawnWave wave)
    {
        // 몬스터 미리 소환
        GameObject enemyPrefab = Instantiate(wave.EnemyPrefab, wave.SpawnPoint.position, wave.SpawnPoint.rotation);
        activedEnemies.Add(enemyPrefab);
        enemyPrefab.SetActive(false);
        // 몬스터가 죽었을 때 호출되는 이벤트 등록
        enemyPrefab.GetComponent<Enemy.Enemy>().OnEnemyDead += DeadEnemy;

        // 대기 시간 후 몬스터 소환
        yield return new WaitForSeconds(wave.SpawnTime);

        enemyPrefab.SetActive(true);
    }

    // 몬스터가 죽었을 때 호출되는 메서드
    private void DeadEnemy(GameObject deadEnemy)
    {
        // 죽은 몬스터를 리스트에서 제거
        activedEnemies.Remove(deadEnemy);

        // 모든 몬스터가 죽었는지 확인
        if(activedEnemies.Count == 0)
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
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundController : MonoBehaviour
{
    [System.Serializable]
    struct EnemySpawnWave
    {
        public float SpawnTime;
        public GameObject EnemyPrefab;
        public Transform SpawnPoint;
    }

    [Header("Round Settings")]
    // 몬스터 소환 웨이브들
    [SerializeField] private EnemySpawnWave[] _enemySpawnWaves;

    // 라운드 클리어 후 다음 라운드에 전달하기 위한 이벤트
    [Header("Complete Round Event")]
    [SerializeField] private UnityEvent _onRoundComplete;

    [Header("Round Walls")]
    // 라운드 벽들
    [SerializeField] private GameObject[] _roundWalls;

    // 라운드가 시작됐는지 여부
    private bool _isRoundStarted = false;
    // 살아있는 몬스터들
    private List<GameObject> _activedEnemies = new List<GameObject>();

    private void Start()
    {
        // 라운드 벽 비활성화
        foreach (GameObject wall in _roundWalls)
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
        if (_isRoundStarted)
            return;

        // 라운드 시작 체크
        _isRoundStarted = true;

        // 라운드 벽 활성화
        foreach (GameObject wall in _roundWalls)
        {
            wall.SetActive(true);
        }

        // 몬스터 소환 등록
        foreach (EnemySpawnWave wave in _enemySpawnWaves)
        {
            StartCoroutine(SpawnEnemyWave(wave));
        }
    }

    // 몬스터 소환 웨이브 등록
    private IEnumerator SpawnEnemyWave(EnemySpawnWave wave)
    {
        // 몬스터 미리 소환
        GameObject enemyPrefab = Instantiate(wave.EnemyPrefab, wave.SpawnPoint.position, wave.SpawnPoint.rotation);
        _activedEnemies.Add(enemyPrefab);
        enemyPrefab.SetActive(false);
        // 몬스터가 죽었을 때 호출되는 이벤트 등록
        enemyPrefab.GetComponent<Character.Enemy.Enemy>().OnEnemyDead += CheckDeadEnemy;

        // 대기 시간 후 몬스터 소환
        yield return new WaitForSeconds(wave.SpawnTime);

        enemyPrefab.SetActive(true);
    }

    // 몬스터가 죽었을 때 호출되는 메서드
    private void CheckDeadEnemy(GameObject deadEnemy)
    {
        // 죽은 몬스터를 리스트에서 제거
        _activedEnemies.Remove(deadEnemy);

        // 모든 몬스터가 죽었는지 확인
        if(_activedEnemies.Count == 0)
        {
            _isRoundStarted = false;
            CompleteRound();
        }
    }

    // 라운드 클리어
    private void CompleteRound()
    {
        Debug.Log("라운드 클리어");

        // 라운드 벽 비활성화
        foreach (GameObject wall in _roundWalls)
        {
            wall.SetActive(false);
        }
        // 라운드 클리어 이벤트 호출
        _onRoundComplete.Invoke();
    }
}
>>>>>>> origin/main
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundController : MonoBehaviour
{
    [System.Serializable]
    struct EnemySpawnWave
    {
        public float SpawnTime;
        public GameObject EnemyPrefab;
        public Transform SpawnPoint;
    }

    [Header("Round Settings")]
    // 몬스터 소환 웨이브들
    [SerializeField] private EnemySpawnWave[] _enemySpawnWaves;

    // 라운드 클리어 후 다음 라운드에 전달하기 위한 이벤트
    [Header("Complete Round Event")]
    [SerializeField] private UnityEvent _onRoundComplete;

    [Header("Round Walls")]
    // 라운드 벽들
    [SerializeField] private GameObject[] _roundWalls;

    // 라운드가 시작됐는지 여부
    private bool _isRoundStarted = false;
    // 살아있는 몬스터들
    private List<GameObject> _activedEnemies = new List<GameObject>();

    private void Start()
    {
        // 라운드 벽 비활성화
        foreach (GameObject wall in _roundWalls)
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
        if (_isRoundStarted)
            return;

        // 라운드 시작 체크
        _isRoundStarted = true;

        // 라운드 벽 활성화
        foreach (GameObject wall in _roundWalls)
        {
            wall.SetActive(true);
        }

        // 몬스터 소환 등록
        foreach (EnemySpawnWave wave in _enemySpawnWaves)
        {
            StartCoroutine(SpawnEnemyWave(wave));
        }
    }

    // 몬스터 소환 웨이브 등록
    private IEnumerator SpawnEnemyWave(EnemySpawnWave wave)
    {
        // 몬스터 미리 소환
        GameObject enemyPrefab = Instantiate(wave.EnemyPrefab, wave.SpawnPoint.position, wave.SpawnPoint.rotation);
        _activedEnemies.Add(enemyPrefab);
        enemyPrefab.SetActive(false);
        // 몬스터가 죽었을 때 호출되는 이벤트 등록
        enemyPrefab.GetComponent<Character.Enemy.Enemy>().OnEnemyDead += CheckDeadEnemy;

        // 대기 시간 후 몬스터 소환
        yield return new WaitForSeconds(wave.SpawnTime);

        enemyPrefab.SetActive(true);
    }

    // 몬스터가 죽었을 때 호출되는 메서드
    private void CheckDeadEnemy(GameObject deadEnemy)
    {
        // 죽은 몬스터를 리스트에서 제거
        _activedEnemies.Remove(deadEnemy);

        // 모든 몬스터가 죽었는지 확인
        if(_activedEnemies.Count == 0)
        {
            _isRoundStarted = false;
            CompleteRound();
        }
    }

    // 라운드 클리어
    private void CompleteRound()
    {
        Debug.Log("라운드 클리어");

        // 라운드 벽 비활성화
        foreach (GameObject wall in _roundWalls)
        {
            wall.SetActive(false);
        }
        // 라운드 클리어 이벤트 호출
        _onRoundComplete.Invoke();
    }
}
>>>>>>> origin/main
