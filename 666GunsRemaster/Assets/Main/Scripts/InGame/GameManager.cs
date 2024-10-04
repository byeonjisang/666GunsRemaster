using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private ReactiveProperty<bool> _isPause = new ReactiveProperty<bool>(false); // �Ͻ����� ���¸� ReactiveProperty�� ����

    [Header("���� ����")]
    public List<string> stages; // �������� ���
    private int currentStageIndex = 0; // ���� �������� �ε���

    [Header("�Ͻ����� �˾�")] 
    public GameObject pauseImage;
    public GameObject menuBtn;

    // �̱���
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

    // ���� �����
    public void Restart()
    {
        // ó�� ������������ ����
        currentStageIndex = 0;
        SceneManager.LoadScene(stages[currentStageIndex]);
        // ReactiveExtensions�� Ȱ���� ���� ��ȭ�� �����ϰ� ó���� �� ����
    }

    public void Pause()
    {
        _isPause.Value = !_isPause.Value; // �Ͻ����� ���¸� ������Ŵ
        pauseImage.SetActive(_isPause.Value);
        menuBtn.SetActive(_isPause.Value);
    }

    private void Start()
    {
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
}