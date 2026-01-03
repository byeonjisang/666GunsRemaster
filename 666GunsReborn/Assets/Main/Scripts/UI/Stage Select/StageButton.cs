using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [Header("Stage Button UI")]
    // 스테이지 이름 텍스트
    [SerializeField] private Text _stageNameText;
    // 버튼 컴포넌트
    [SerializeField] private Button _button;
    // 스테이지 잠금 이미지
    [SerializeField] private GameObject _lockImage;


    // 스테이지 데이터로 버튼 초기화
    public void Init(int stageIndex, bool isCleared)
    {
        // 스테이지 별 이름 설정
        _stageNameText.text = "Stage " + stageIndex;
        // 스테이지 클리어 여부에 따라 잠금 이미지 활성화 설정 맟 버튼 상호작용 설정
        _lockImage.SetActive(!isCleared);
        _button.interactable = isCleared;
    }
}