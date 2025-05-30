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

    [Header("Result UI")]
    [SerializeField]
    private GameObject resultUI;
    [SerializeField]
    private Text clearTimeText;

    private void Start()
    {
        //resultUI.SetActive(false);
    }

    public void UpdateTimer(float time){
        int min = (int)time / 60;
        int sec = (int)time % 60;
        //timerText.text = min.ToString("D2") + ":" + sec.ToString("D2");
    }

    public void ShowStageClearUI(float clearTime){
        //resultUI.SetActive(true);
        int clearTimeMin = (int)clearTime / 60;
        int clearTimeSec = (int)clearTime % 60;
        //clearTimeText.text = "Clear Time : " + clearTimeMin.ToString("D2") + ":" + clearTimeSec.ToString("D2");
    }

    public void ReturnToLobby(){
        SceneManager.LoadScene("Lobby");
    }
}