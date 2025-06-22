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
        // �޸� ���� ������ ���� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void InitializeQuestUI()
    {
        // ���� �ִ� ������Ʈ���� ã�� �ڵ����� �Ҵ�
        if (questCanvas == null)
        {
            questCanvas = GameObject.Find("QuestCanvas");
        }

        if (questTextParent == null && questCanvas != null)
        {
            questTextParent = questCanvas.transform.Find("QuestPanel");

        }

        if (questTexts.Count > 0) return; // �̹� �����Ǿ����� ����� �� ��

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


