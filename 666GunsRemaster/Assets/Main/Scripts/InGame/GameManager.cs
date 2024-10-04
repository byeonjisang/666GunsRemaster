using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private ReactiveProperty<bool> _isPause = new ReactiveProperty<bool>(false); // 일시정지 상태를 ReactiveProperty로 변경

    [Header("게임 설정")]
    public List<string> stages; // 스테이지 목록
    private int currentStageIndex = 0; // 현재 스테이지 인덱스

    [Header("일시정지 팝업")] 
    public GameObject pauseImage;
    public GameObject menuBtn;

    // 싱글턴
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

    // 게임 재시작
    public void Restart()
    {
        // 처음 스테이지에서 시작
        currentStageIndex = 0;
        SceneManager.LoadScene(stages[currentStageIndex]);
        // ReactiveExtensions를 활용해 상태 변화를 감지하고 처리할 수 있음
    }

    public void Pause()
    {
        _isPause.Value = !_isPause.Value; // 일시정지 상태를 반전시킴
        pauseImage.SetActive(_isPause.Value);
        menuBtn.SetActive(_isPause.Value);
    }

    private void Start()
    {
        // UniRx로 일시정지 상태 변화를 구독
        _isPause.Subscribe(isPaused =>
        {
            if (isPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }).AddTo(this); // 구독을 GameManager에 추가하여 게임 오브젝트가 파괴될 때 자동으로 구독 해제
    }
}