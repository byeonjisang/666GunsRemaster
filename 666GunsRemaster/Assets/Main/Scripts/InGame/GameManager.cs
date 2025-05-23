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
    public GameObject gameOverObject;

    //제한시간은 6분 고정.
    public Text timerText;
    float timer = 300f;

    private float deathTime = 0f;

    public float GetTimer() {  return timer; }


    // 싱글턴
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject); // 이미 인스턴스가 있으면 새로 생성된 GameManager는 파괴
        }
    }

    // 씬 로드 후 일시정지 상태 초기화
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드될 때 pauseImage 및 menuBtn을 다시 찾아 할당
        pauseImage = GameObject.Find("Pause");
        player = GameObject.Find("Player");
        gameOverObject = GameObject.Find("GameOver");

        // 새로운 씬에서 pauseBtn을 찾아 이벤트 리스너를 등록
        pauseBtn = GameObject.Find("PauseButton")?.GetComponent<Button>();

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

        Time.timeScale = 1f; // Time.timeScale도 원래 상태로 되돌림

        //인게임 사운드 재생
        if (scene.name == "Main Scene")
        {
            SoundManager.instance.PlayBGMSound(1);
            Debug.Log("인게임 브금 재생");
        }

        timer = 300;
        deathTime = 0f;
    }
    void GameOver()
    {
        //플레이어가 죽으면
        if(PlayerController.Instance != null && PlayerController.Instance.IsDie.Value == true)
        {
            deathTime += Time.deltaTime;
            Debug.Log(deathTime);
            SoundManager.instance.StopBGMSound(1);

            if (deathTime > 3f)
            {
                SoundManager.instance.PlayBGMSoundOnce(2);
                //플레이어 없어지고 게임오버 창 활성화
                if (gameOverObject != null)
                {
                    gameOverObject.SetActive(true);
                    Debug.Log("Game over UI activated");
                }
                deathTime = 0f;

                //Restart();
            }
        }
    }

    public void GameClear()
    {
        if(timer <= 0)
        {
            SoundManager.instance.StopAllSound();

            //일단 씬만 이동하는걸로 설정
            SceneManager.LoadScene("Ending Scene");
        }
    }

    // 게임 재시작
    //public void Restart()
    //{
    //    // 처음 스테이지에서 시작
    //    currentStageIndex = 0;
    //    timer = 360f; // 타이머 초기화
    //    deathTime = 0f; // 죽은 시간 초기화

    //    // 플레이어 상태 초기화
    //    //if (PlayerController.Instance != null)
    //    //{
    //    //    PlayerController.Instance.Revive(); // 플레이어 죽음 상태 초기화
    //    //}

    //    // 일시정지 상태 초기화
    //    _isPause.Value = false;
    //    Time.timeScale = 1f; // 타임스케일 초기화

    //    // 모든 UI 및 오브젝트 상태 초기화
    //    if (pauseImage != null) pauseImage.SetActive(false);
    //    if (menuBtn != null) menuBtn.SetActive(false);
    //    if (gameOverObject != null) gameOverObject.SetActive(false);
    //    if (player != null) player.SetActive(true); // 플레이어를 다시 활성화

    //    // 씬을 다시 로드
    //    //SceneManager.LoadScene(stages[currentStageIndex]);

    //    // 배경음 초기화 및 재생
    //    //SoundManager.instance.PlayBGMSound(1);
    //    Debug.Log("게임이 재시작되었습니다.");
    //}


    public void Pause()
    {
        Time.timeScale = 0f;
        _isPause.Value = !_isPause.Value; // 일시정지 상태를 반전시킴
        pauseImage.SetActive(_isPause.Value);
        Debug.Log("일시정지 누름");
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        _isPause.Value = !_isPause.Value; // 일시정지 상태를 반전시킴
        pauseImage.SetActive(_isPause.Value);
        Debug.Log("계속 진행");
    }

    public void Timer()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Max(timer, 0); // 타이머가 음수가 되지 않도록 보정
    }

    void Start()
    {
        SoundManager.instance.PlayBGMSound(1);

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

        //타이머들 초기화
        timer = 300;
        deathTime = 0f;
    }

    void Update()
    {
        // 분과 초로 변환
        int minutes = Mathf.FloorToInt(timer / 60); // 분 계산
        int seconds = Mathf.FloorToInt(timer % 60); // 초 계산

        // 텍스트 UI 업데이트
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // "MM:SS" 형식으로 출력

        //테스트를 위한 치트
        if(Input.GetKey(KeyCode.O))
        {
            Debug.Log("치트키 사용 중");
            timer -= Time.deltaTime * 50;
        }

        GameClear();
        GameOver();
    }

    private void OnDestroy()
    {
        // 씬 로드 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
