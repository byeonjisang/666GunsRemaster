using System.Collections;
using System.Collections.Generic;
using StageSelect;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageSelectPresenter
{
    // 뷰
    private StageSelectView _view;

    // 스테이지 선택 후 이벤트
    public System.Action<StageData> OnStageSelected;

    // 생성자
    public StageSelectPresenter(StageSelectView view)
    {
        _view = view;
    }

    // 스테이지 버튼 클릭 시 호출되는 메서드
    public void OnStageButtonClicked(StageData stageData)
    {
        OnStageSelected?.Invoke(stageData);
    }
}
