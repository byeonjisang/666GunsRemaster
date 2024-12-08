using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePosition : MonoBehaviour
{
    public List<GameObject> posList = new List<GameObject>();
    public Text timer;

    private int currentPosIndex = 0;
    private int lastMinute = -1; // 마지막으로 확인한 분 단위 시간

    void Start()
    {
        // 모든 위치를 비활성화하고 첫 번째 위치만 활성화
        for (int i = 0; i < posList.Count; i++)
        {
            posList[i].SetActive(i == 0);
        }
        currentPosIndex = 0;
        lastMinute = Mathf.FloorToInt(GameManager.instance.GetTimer() / 60f); // 초기 분 값 설정
    }

    void Update()
    {
        // 남은 시간을 분 단위로 확인
        int minutesRemaining = Mathf.FloorToInt(GameManager.instance.GetTimer() / 60f);

        if (minutesRemaining != lastMinute && minutesRemaining != 4)
        {
            // 분이 줄어들면 다음 거점을 활성화
            SetPosition(currentPosIndex + 1);
            lastMinute = minutesRemaining;
        }
    }

    private void SetPosition(int index)
    {
        if (index < posList.Count)
        {
            //사운드 재생
            SoundManager.instance.PlayEffectSoundOnce(12);

            // 이전 위치 비활성화
            posList[currentPosIndex].SetActive(false);

            //버프 선택
            BuffManager.Instance.SelectBuff();

            // 새로운 위치 활성화
            posList[index].SetActive(true);
            currentPosIndex = index;
        }
        else
        {
            // 모든 위치를 비활성화하고 인덱스 초기화
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
