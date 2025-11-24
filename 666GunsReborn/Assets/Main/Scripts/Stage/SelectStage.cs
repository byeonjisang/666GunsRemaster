using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    [SerializeField]
    private List<Button> stageButtons;
    [SerializeField]
    private List<GameObject> checkStageClearImages;
    [SerializeField]
    private List<GameObject> rockStageImages;

    private void Start()
    {
        CheckStageState();
    }

    public void StageSelect(int stageIndex)
    {
        StageManager.Instance.StartStage(stageIndex);
    }

    public void CheckStageState(){
        List<bool> isStageClear = StageManager.Instance.GetStageClearState();

        // 스테이지 클리어 및 잠금 미리 비활성화
        for (int i = 0; i < stageButtons.Count; i++)
        {
            stageButtons[i].interactable = true;
            checkStageClearImages[i].SetActive(false);
            rockStageImages[i].SetActive(false);
        }

        // 스테이지 클리어 상태에 따라 이미지 표시
        int isNotClearStageIndex = -1;
        for (int i = 0; i < isStageClear.Count; i++)
        {
            if (isStageClear[i])
            {
                checkStageClearImages[i].SetActive(true);
            }
            else
            {
                if(isNotClearStageIndex == -1){
                    isNotClearStageIndex = i;
                    continue;
                }
                    
                rockStageImages[i].SetActive(true);
                stageButtons[i].interactable = false;
            }
        }
    }
}
