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
            foreach (var stageData in allStageData)
            {
                // 버튼 생성 및 초기화
                GameObject stageButtonObject = Instantiate(_stageButtonPrefab, _stageButtonParent);
                StageButton stageButton = stageButtonObject.GetComponent<StageButton>();
                stageButton.Init(stageData);

                // 버튼 클릭 리스너 설정
                Button buttonComponent = stageButtonObject.GetComponent<Button>();
                buttonComponent.onClick.AddListener(() => 
                     Presenter.OnStageButtonClicked(stageData));
            }
        }
    }    
}