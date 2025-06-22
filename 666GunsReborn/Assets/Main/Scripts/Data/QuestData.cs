using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// ����Ʈ�� ������ �����ϴ� Ŭ����
/// </summary>
[CreateAssetMenu(fileName = "QuestData", menuName = "Datas/QuestData", order = 1)]
public class QuestData : ScriptableObject
{
    public int questId;
    public bool isOptionalQuestType;

    /// �� ����Ʈ�� ����Ǵ� ���� �̸� (��: "Stage1", "BossRoom")
    public string questScene;

    /// <summary>
    /// �� ����Ʈ�� �䱸�ϴ� ���� ��ǥ ���
    /// ������ ��ǥ�� QuestBase�� ����� ���� ����Ʈ ��Ʈ (��: KillTargets_Quest, StageClear_Quest ��)�� ������
    /// </summary>
    public QuestBase[] allQuests;

    /// ����Ʈ�� ���� ���� ���� (���� �� / ���� �� / �Ϸ� ��)
    public QuestState questState = QuestState.NEVER_RECEIVED;
}
