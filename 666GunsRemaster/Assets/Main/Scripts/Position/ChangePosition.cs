using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePosition : MonoBehaviour
{
    public List<GameObject> posList = new List<GameObject>();
    public Text timer;

    private int currentPosIndex = 0;
    private int lastMinute = -1; // ���������� Ȯ���� �� ���� �ð�

    void Start()
    {
        // ��� ��ġ�� ��Ȱ��ȭ�ϰ� ù ��° ��ġ�� Ȱ��ȭ
        for (int i = 0; i < posList.Count; i++)
        {
            posList[i].SetActive(i == 0);
        }
        currentPosIndex = 0;
        lastMinute = Mathf.FloorToInt(GameManager.instance.GetTimer() / 60f); // �ʱ� �� �� ����
    }

    void Update()
    {
        // ���� �ð��� �� ������ Ȯ��
        int minutesRemaining = Mathf.FloorToInt(GameManager.instance.GetTimer() / 60f);

        if (minutesRemaining != lastMinute && minutesRemaining != 4)
        {
            // ���� �پ��� ���� ������ Ȱ��ȭ
            SetPosition(currentPosIndex + 1);
            lastMinute = minutesRemaining;
        }
    }

    private void SetPosition(int index)
    {
        if (index < posList.Count)
        {
            //���� ���
            SoundManager.instance.PlayEffectSoundOnce(12);

            // ���� ��ġ ��Ȱ��ȭ
            posList[currentPosIndex].SetActive(false);

            //���� ����
            BuffManager.Instance.SelectBuff();

            // ���ο� ��ġ Ȱ��ȭ
            posList[index].SetActive(true);
            currentPosIndex = index;
        }
        else
        {
            // ��� ��ġ�� ��Ȱ��ȭ�ϰ� �ε��� �ʱ�ȭ
            DeactivateAllPositions();
        }
    }

    private void DeactivateAllPositions()
    {
        foreach (GameObject pos in posList)
        {
            pos.SetActive(false);
        }
        currentPosIndex = 0;
    }
}
