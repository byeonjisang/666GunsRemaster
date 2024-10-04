using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private bool _isPause = false;

    [Header("게임 설정")]
    public List<string> stages; // 스테이지 목록
    private int currentStageIndex = 0; // 현재 스테이지 인덱스

    //싱글턴
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    //게임 재시작
    public void Restart()
    {
        //맨 처음 스테이지로 이동
        currentStageIndex = 0;
        SceneManager.LoadScene(stages[currentStageIndex]);
        //currentLives = playerLives;
        //currentHealth = playerHealth;
        //RespawnPlayer();
    }

    public void Pause()
    {
        if (!_isPause)
        {
            Time.timeScale = 0f;
            _isPause = true;
        }
        else
        {
            Time.timeScale = 1f; // 게임 시간을 다시 흐르게 함
            _isPause = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
