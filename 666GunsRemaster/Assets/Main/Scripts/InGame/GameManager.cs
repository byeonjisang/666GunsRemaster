using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private ReactiveProperty<bool> _isPause = new ReactiveProperty<bool>(false); // 일시정지 상태를 ReactiveProperty로 변경

    [Header("게임 설정")]
    public List<string> stages; // 스테이지 목록
    private int currentStageIndex = 0; // 현재 스테이지 인덱스

    [Header("일시정지 팝업")]
    [SerializeField]
    private Button pauseBtn;

    public GameObject pauseImage;
    public GameObject menuBtn;

    // 싱글턴
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있으면 새로 생성된 GameManager는 파괴
        }
    }

    // 씬 로드 후 일시정지 상태 초기화
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드될 때 pauseImage 및 menuBtn을 다시 찾아 할당
        pauseImage = GameObject.Find("PauseImage"); // PauseImage라는 이름을 가진 오브젝트를 찾음
        menuBtn = GameObject.Find("ExitMenu");

        // 새로운 씬에서 pauseBtn을 찾아 이벤트 리스너를 등록
        pauseBtn = GameObject.Find("Pause")?.GetComponent<Button>();

        if (pauseBtn != null)
        {
            pauseBtn.onClick.AddListener(Pause);
        }

        // 일시정지 상태 초기화
        _isPause.Value = false;

        if (pauseImage != null)
        {
            pauseImage.SetActive(false);
        }

        if (menuBtn != null)
        {
            menuBtn.SetActive(false);
        }

        Time.timeScale = 1f; // Time.timeScale도 원래 상태로 되돌림
    }


    // 게임 재시작
    public void Restart()
    {
        // 처음 스테이지에서 시작
        currentStageIndex = 0;
        SceneManager.LoadScene(stages[currentStageIndex]);
    }

    public void Pause()
    {
        _isPause.Value = !_isPause.Value; // 일시정지 상태를 반전시킴
        pauseImage.SetActive(_isPause.Value);
        menuBtn.SetActive(_isPause.Value);
        Debug.Log("일시정지 누름");
    }

    void Start()
    {
        // 씬 로드 이벤트에 메서드 등록
        SceneManager.sceneLoaded += OnSceneLoaded;

        //일시정지에 이벤트 등록
        if (pauseBtn != null)
        {
            pauseBtn.onClick.AddListener(Pause);
        }

        if (pauseImage != null)
        {
            pauseImage.SetActive(false);
        }

        if (menuBtn != null)
        {
            menuBtn.SetActive(false);
        }

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

    private void OnDestroy()
    {
        // 씬 로드 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
