using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Player UI")]
    [SerializeField]
    private Button fireButton;
    [SerializeField]
    private Button dashButton;

    [Header("Stage UI")]
    [SerializeField]
    private Text timerText;

    [Header("Claer UI")]
    [SerializeField]
    private GameObject clearUI;
    [SerializeField]
    private Text clearTimeText;

    [Header("Failed UI")]
    [SerializeField]
    private GameObject failedUI;

    bool isRead = false;

    private void Start()
    {
        //clearUI.SetActive(false);
        //failedUI.SetActive(false);

        isRead = true;
    }

    public void UpdateTimer(float time){
        if (isRead)
        {
            int min = (int)time / 60;
            int sec = (int)time % 60;
            timerText.text = min.ToString("D2") + ":" + sec.ToString("D2");
        }
        
    }

    public void ShowStageClearUI(float clearTime){
        clearUI.SetActive(true);
        int clearTimeMin = (int)clearTime / 60;
        int clearTimeSec = (int)clearTime % 60;
        clearTimeText.text = "Clear Time : " + clearTimeMin.ToString("D2") + ":" + clearTimeSec.ToString("D2");
    }

    public void ShowFailedUI()
    {
        failedUI.SetActive(true);
    }

    public void ReturnToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}