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
    private ReactiveProperty<bool> _isPause = new ReactiveProperty<bool>(false); // �Ͻ����� ���¸� ReactiveProperty�� ����

    [Header("���� ����")]
    public List<string> stages; // �������� ���
    private int currentStageIndex = 0; // ���� �������� �ε���

    [Header("�Ͻ����� �˾�")]
    [SerializeField]
    private Button pauseBtn;

    public GameObject player;
    public GameObject pauseImage;
    public GameObject menuBtn;
    public GameObject gameOverObject;

    //���ѽð��� 6�� ����.
    public Text timerText;
    float timer = 305f;

    private float deathTime = 0f;

    public float GetTimer() {  return timer; }


    // �̱���
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject); // �̹� �ν��Ͻ��� ������ ���� ������ GameManager�� �ı�
        }
    }

    // �� �ε� �� �Ͻ����� ���� �ʱ�ȭ
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�� �� pauseImage �� menuBtn�� �ٽ� ã�� �Ҵ�
        pauseImage = GameObject.Find("PauseImage"); // PauseImage��� �̸��� ���� ������Ʈ�� ã��
        menuBtn = GameObject.Find("ExitMenu");
        player = GameObject.Find("Player");
        pauseImage = GameObject.Find("PauseImage");
        gameOverObject = GameObject.Find("GameOver");

        // ���ο� ������ pauseBtn�� ã�� �̺�Ʈ �����ʸ� ���
        pauseBtn = GameObject.Find("Pause")?.GetComponent<Button>();

        if (pauseBtn != null)
        {
            pauseBtn.onClick.AddListener(Pause);
        }

        // �Ͻ����� ���� �ʱ�ȭ
        _isPause.Value = false;

        if (pauseImage != null)
        {
            pauseImage.SetActive(false);
        }

        if (menuBtn != null)
        {
            menuBtn.SetActive(false);
        }

        Time.timeScale = 1f; // Time.timeScale�� ���� ���·� �ǵ���

        //�ΰ��� ���� ���
        if (scene.name == "Main Scene")
        {
            SoundManager.instance.PlayBGMSound(1);
            Debug.Log("�ΰ��� ��� ���");
        }

        timer = 305;
        deathTime = 0f;
    }
    void GameOver()
    {
        //�÷��̾ ������
        if(PlayerController.Instance != null && PlayerController.Instance.IsDie.Value == true)
        {
            deathTime += Time.deltaTime;
            Debug.Log(deathTime);

            if (deathTime > 3f)
            {
                //�÷��̾� �������� ���ӿ��� â Ȱ��ȭ
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

    // ���� �����
    //public void Restart()
    //{
    //    // ó�� ������������ ����
    //    currentStageIndex = 0;
    //    timer = 360f; // Ÿ�̸� �ʱ�ȭ
    //    deathTime = 0f; // ���� �ð� �ʱ�ȭ

    //    // �÷��̾� ���� �ʱ�ȭ
    //    //if (PlayerController.Instance != null)
    //    //{
    //    //    PlayerController.Instance.Revive(); // �÷��̾� ���� ���� �ʱ�ȭ
    //    //}

    //    // �Ͻ����� ���� �ʱ�ȭ
    //    _isPause.Value = false;
    //    Time.timeScale = 1f; // Ÿ�ӽ����� �ʱ�ȭ

    //    // ��� UI �� ������Ʈ ���� �ʱ�ȭ
    //    if (pauseImage != null) pauseImage.SetActive(false);
    //    if (menuBtn != null) menuBtn.SetActive(false);
    //    if (gameOverObject != null) gameOverObject.SetActive(false);
    //    if (player != null) player.SetActive(true); // �÷��̾ �ٽ� Ȱ��ȭ

    //    // ���� �ٽ� �ε�
    //    //SceneManager.LoadScene(stages[currentStageIndex]);

    //    // ����� �ʱ�ȭ �� ���
    //    //SoundManager.instance.PlayBGMSound(1);
    //    Debug.Log("������ ����۵Ǿ����ϴ�.");
    //}


    public void Pause()
    {
        _isPause.Value = !_isPause.Value; // �Ͻ����� ���¸� ������Ŵ
        pauseImage.SetActive(_isPause.Value);
        menuBtn.SetActive(_isPause.Value);
        Debug.Log("�Ͻ����� ����");
    }

    public void Timer()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Max(timer, 0); // Ÿ�̸Ӱ� ������ ���� �ʵ��� ����
    }

    void Start()
    {
        SoundManager.instance.PlayBGMSound(1);

        // �� �ε� �̺�Ʈ�� �޼��� ���
        SceneManager.sceneLoaded += OnSceneLoaded;

        //�Ͻ������� �̺�Ʈ ���
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

        // UniRx�� �Ͻ����� ���� ��ȭ�� ����
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
        }).AddTo(this); // ������ GameManager�� �߰��Ͽ� ���� ������Ʈ�� �ı��� �� �ڵ����� ���� ����

        //Ÿ�̸ӵ� �ʱ�ȭ
        timer = 305;
        deathTime = 0f;
    }

    void Update()
    {
        // �а� �ʷ� ��ȯ
        int minutes = Mathf.FloorToInt(timer / 60); // �� ���
        int seconds = Mathf.FloorToInt(timer % 60); // �� ���

        // �ؽ�Ʈ UI ������Ʈ
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // "MM:SS" �������� ���

        GameOver();
    }

    private void OnDestroy()
    {
        // �� �ε� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
