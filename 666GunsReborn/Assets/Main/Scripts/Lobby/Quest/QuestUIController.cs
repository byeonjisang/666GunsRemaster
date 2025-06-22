using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestUIController : Singleton<QuestUIController>
{
    protected override bool IsPersistent => true;

    public QuestData questData;
    public GameObject questTextPrefab;
    public Transform questTextParent;
    private List<Text> questTexts = new List<Text>();

    public GameObject questCanvas;

    void Start() => InitializeQuestUI();

    void Update() => UpdateQuestUI();

    protected override void Awake()
    {
        base.Awake();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        questTexts.Clear();
        InitializeQuestUI();
    }
    void OnDestroy()
    {
        // 메모리 누수 방지를 위해 이벤트 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void InitializeQuestUI()
    {
        // 씬에 있는 오브젝트들을 찾아 자동으로 할당
        if (questCanvas == null)
        {
            questCanvas = GameObject.Find("QuestCanvas");
        }

        if (questTextParent == null && questCanvas != null)
        {
            questTextParent = questCanvas.transform.Find("QuestPanel");

        }

        if (questTexts.Count > 0) return; // 이미 생성되었으면 재생성 안 함

        foreach (var quest in questData.allQuests)
        {
            var go = Instantiate(questTextPrefab, questTextParent);
            var text = go.GetComponent<Text>();
            text.text = quest.GetFormatText();
            questTexts.Add(text);
        }
    }

    void UpdateQuestUI()
    {
        for (int i = 0; i < questTexts.Count; i++)
        {
            questTexts[i].text = questData.allQuests[i].GetFormatText();
        }
    }

    public void ExitQuestUI()
    {
        questCanvas.SetActive(false);
    }
}


