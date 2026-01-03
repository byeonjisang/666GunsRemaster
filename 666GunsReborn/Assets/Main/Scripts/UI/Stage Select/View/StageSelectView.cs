using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StageSelect
{
    public class StageSelectView : MonoBehaviour
    {
        // 스테이지 UI 프리팹
        [Header("Stage Button Prefab")]
        [SerializeField] private GameObject _stageButtonPrefab;
        
        // 스테이지 버튼 부모 오브젝트
        [Header("Stage Button Parent")]
        [SerializeField] private Transform _stageButtonParent;

        // StageSelect 중재자
        public StageSelectPresenter Presenter { get; private set; }

        public void Init(List<StageData> allStageData)
        {
            Presenter = new StageSelectPresenter(this);
            CreateStageButtons(allStageData);
        }

        // 스테이지 버튼 생성 메서드
        private void CreateStageButtons(List<StageData> allStageData)
        {
            bool before_cleared = false;
            for(int i = 0; i < allStageData.Count; i++)
            {
                // allStageData[i]를 사용하면 i를 참조하여 ArrayIndexOutOfBounds 예외가 발생할 수 있으므로 별도의 변수에 할당
                StageData stageData = allStageData[i];

                // 버튼 생성 및 초기화
                GameObject stageButtonObject = Instantiate(_stageButtonPrefab, _stageButtonParent);
                StageButton stageButton = stageButtonObject.GetComponent<StageButton>();
                // 1스테이지는 무조건 오픈, 그 외에는 이전 스테이지 클리어 여부에 따라 설정
                stageButton.Init(stageData.stageIndex, i == 0 ? true : before_cleared);
                before_cleared = stageData.isCleared;

                // 버튼 클릭 리스너 설정
                Button buttonComponent = stageButtonObject.GetComponent<Button>();
                buttonComponent.onClick.AddListener(() => 
                     Presenter.OnStageButtonClicked(stageData));
            }
        }
    }    
}