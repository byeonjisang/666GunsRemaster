using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private bool _isPause = false;

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
        SceneManager.LoadScene("Practice");
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
