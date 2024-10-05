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
    private ReactiveProperty<bool> _isPause = new ReactiveProperty<bool>(false); // �Ͻ����� ���¸� ReactiveProperty�� ����

    [Header("���� ����")]
    public List<string> stages; // �������� ���
    private int currentStageIndex = 0; // ���� �������� �ε���

    [Header("�Ͻ����� �˾�")]
    [SerializeField]
    private Button pauseBtn;

    public GameObject pauseImage;
    public GameObject menuBtn;

    // �̱���
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� ������ ���� ������ GameManager�� �ı�
        }
    }

    // �� �ε� �� �Ͻ����� ���� �ʱ�ȭ
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�� �� pauseImage �� menuBtn�� �ٽ� ã�� �Ҵ�
        pauseImage = GameObject.Find("PauseImage"); // PauseImage��� �̸��� ���� ������Ʈ�� ã��
        menuBtn = GameObject.Find("ExitMenu");

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
    }


    // ���� �����
    public void Restart()
    {
        // ó�� ������������ ����
        currentStageIndex = 0;
        SceneManager.LoadScene(stages[currentStageIndex]);
    }

    public void Pause()
    {
        _isPause.Value = !_isPause.Value; // �Ͻ����� ���¸� ������Ŵ
        pauseImage.SetActive(_isPause.Value);
        menuBtn.SetActive(_isPause.Value);
        Debug.Log("�Ͻ����� ����");
    }

    void Start()
    {
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
    }

    private void OnDestroy()
    {
        // �� �ε� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
