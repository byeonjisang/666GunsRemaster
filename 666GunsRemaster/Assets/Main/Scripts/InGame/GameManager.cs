using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UnityEngine.UI;
using Character.Player;

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

    public GameObject player;
    public GameObject pauseImage;
    public GameObject menuBtn;
    public GameObject gameOverObject;

    //제한시간은 6분 고정.
    public Text timerText;
    float timer = 360f;

    public float GetTimer() {  return timer; }


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

        //인게임 사운드 재생
        if (scene.name == "Main Scene")
        {
            SoundManager.instance.PlayBGMSound(1);
        }
    }
    void GameOver()
    {
        //플레이어가 죽으면
        if(PlayerController.Instance.GetIsDie() == true)
        {
            float deathTime = 0f;
            deathTime += Time.deltaTime * 500;
            Debug.Log(deathTime);

            if (deathTime > 1f)
            {
                //플레이어 없어지고 게임오버 창 활성화
                player.SetActive(false);
                gameOverObject.SetActive(true);
                deathTime = 0f;
            }
        }
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

    public void Timer()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Max(timer, 0); // 타이머가 음수가 되지 않도록 보정
    }

    void Start()
    {
        //SoundManager.instance.PlayBGMSound(1);

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

    void Update()
    {
        // 분과 초로 변환
        int minutes = Mathf.FloorToInt(timer / 60); // 분 계산
        int seconds = Mathf.FloorToInt(timer % 60); // 초 계산

        // 텍스트 UI 업데이트
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // "MM:SS" 형식으로 출력

        GameOver();
    }

    private void OnDestroy()
    {
        // 씬 로드 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
