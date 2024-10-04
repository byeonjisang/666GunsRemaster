using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private bool _isPause = false;

    [Header("���� ����")]
    public List<string> stages; // �������� ���
    private int currentStageIndex = 0; // ���� �������� �ε���

    //�̱���
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
    //���� �����
    public void Restart()
    {
        //�� ó�� ���������� �̵�
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
            Time.timeScale = 1f; // ���� �ð��� �ٽ� �帣�� ��
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
