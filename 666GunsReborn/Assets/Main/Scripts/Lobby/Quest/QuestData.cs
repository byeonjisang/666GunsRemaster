using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ʈ�� ������ �����ϴ� Ŭ����
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "Quest", menuName = "Add Quest/Quest")]
public class QuestData : ScriptableObject
{
    [HideInInspector] public QuestState questState = QuestState.NEVER_RECEIVED;

    [Header("������ ����Ʈ�� ID")]
    [SerializeField] public int questId;
    [Space(50)]

    ///����Ʈ �߰� �� �ۼ� ����
    [Header("����Ʈ ��ǥ")]
    //[SerializeField] public Quest_KillTargets[] killTargetsQuests; //���� óġ�ϴ� ��ǥ
    //[SerializeField] public Quest_GetItems[] getItemsQuests; //�������� ȹ���ϴ� ��ǥ

    [Space(50)]
    [Header("����Ʈ�� ��ǥ �� �ϳ��� �����ص� �Ǵ� Ÿ���ΰ�?")]
    [SerializeField] public bool isOptionalQuestType;

    [Space(10)]
    [Header("����Ʈ�� �Ϸ� �� ���� ����ġ")]
    [SerializeField] public float expAmount;

    [Space(10)]
    [Header("����Ʈ�� �����ϴ� �� �̸�")] public string questScene;
    [Header("����Ʈ�� �Ƿ��� ����� ��ġ")] public Vector3 sourcePos;
    [Header("����Ʈ�� �����Ϸ� ���� �� ������ ��ġ")] public Vector3 destinationPos;

    /// <summary>
    /// �ش� ����Ʈ�� ������ ��� �κ��� ����Ʈ
    /// </summary>
    public QuestBase[] allQuests;
}
