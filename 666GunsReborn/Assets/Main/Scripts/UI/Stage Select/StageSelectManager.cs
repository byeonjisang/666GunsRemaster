using System.Collections.Generic;
using StageSelect;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    [Header("Stage Select Data")]
    // 스테이지 선택한 결과 저장 데이터
    [SerializeField]private StageSelectData _stageSelectData;

    [Header("Stage Select GameObject")]
    [SerializeField] private GameObject _stageSelectGameObject;

    [Header("Views")]
    [SerializeField] private StageSelectView _stageSelectView;
    [SerializeField] private WeaponEquipView _weaponEquipView;
    [SerializeField] private WeaponSelectView _weaponSelectView;
    [SerializeField] private ExitView _exitView;

    // 나나기 버튼 클릭 시 한 단계씩 나가기 위한 스택
    private Stack<GameObject> _uiStack = new Stack<GameObject>();
    
    // 현재 선택 중인 무기 인덱스
    private int _currentWeaponIndex = 0;

    private void Start()
    {
        // 스테이지 데이터 로드
        AddressablesLoader.LoadAssetByLabel<StageData>("StageData", (stageDatas) =>
            {
                if(stageDatas != null)
                {
                    Debug.Log("Stage Data Loaded: " + stageDatas.Count);
                    stageDatas.Sort((a, b) => a.stageIndex.CompareTo(b.stageIndex));
                    _stageSelectView.Init(stageDatas);

                    // 스테이지 선택 후 이벤트 구독
                    _stageSelectView.Presenter.OnStageSelected += HandleStageSelected;
                }
            });

        // 웨폰 데이터 로드
        AddressablesLoader.LoadAssetByLabel<WeaponData>("WeaponData", (weaponDatas) =>
            {
                if(weaponDatas != null)
                {
                    Debug.Log("Weapon Data Loaded: " + weaponDatas.Count);
                    _weaponSelectView.Init(weaponDatas);
                    _weaponEquipView.Init(_stageSelectData.SelectedWeaponsID);

                    // 무기 선택 후 이벤트 구독
                    _weaponEquipView.Presenter.WeaponEquipChanged += TransitionToWeaponSelect;
                    // 무기 변경 후 이벤트 구독
                    _weaponSelectView.Presenter.OnWeaponSelected += (weaponData) =>
                    {
                        Debug.Log("Weapon Selected: " + weaponData.weaponID);
                        _stageSelectData.SelectedWeaponsID[_currentWeaponIndex] = weaponData.weaponID;
                        _weaponEquipView.UpdateWeaponEquip(_stageSelectData.SelectedWeaponsID);
                        _weaponSelectView.gameObject.SetActive(false);
                        // 나가기 버튼 활성화(TODO : 만약 무기 선택 중에 나가기가 가능하면 이거 없애야 함)
                        _exitView.ExitButtonInteractable(true);
                    };
                    // 게임 시작 요청 이벤트 구독
                    _weaponEquipView.Presenter.StageStartRequested += StageStart;
                }
            });

        // 종료 뷰 초기화
        _exitView.Init();
        _exitView.Presenter.OnExitRequested += HandleExitRequested;

        // 초기 UI 설정
        _uiStack.Push(_stageSelectView.gameObject);
        _stageSelectView.gameObject.SetActive(true);
        _weaponEquipView.gameObject.SetActive(false);
        _weaponSelectView.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // 이벤트 해체
        if(_stageSelectView.Presenter != null)
        {
            _stageSelectView.Presenter.OnStageSelected = null;
        }
        if(_weaponEquipView.Presenter != null)
        {
            _weaponEquipView.Presenter.WeaponEquipChanged = null;
            _weaponEquipView.Presenter.StageStartRequested = null;
        }
        if(_weaponSelectView.Presenter != null)
        {
            _weaponSelectView.Presenter.OnWeaponSelected = null;
        }
    }

    // 스테이지 선택 후 처리 메서드
    private void HandleStageSelected(StageData data)
    {
        // 선택한 스테이지 데이터 저장
        _stageSelectData.StageData = data;

        // 무기 선택 창으로 이동
        Debug.Log("Selected Stage: " + data.stageName);
        _stageSelectView.gameObject.SetActive(false);
        _weaponEquipView.gameObject.SetActive(true);
        _uiStack.Push(_weaponEquipView.gameObject);
    }

    // 무기 선택 창으로 전환 메서드
    private void TransitionToWeaponSelect(int index)
    {
       AddressablesLoader.LoadAssetByLabel<WeaponData>("WeaponData", (weaponDatas) =>
            {
                if(weaponDatas != null)
                {
                    Debug.Log("Weapon Data Loaded: " + weaponDatas.Count);
                    
                    _weaponSelectView.gameObject.SetActive(true);
                    _currentWeaponIndex = index;

                    WeaponData weaponIndex = weaponDatas.Find(w => w.weaponID == _stageSelectData.SelectedWeaponsID[index]);
                    _weaponSelectView.UpdateWeaponInfo(weaponIndex);
                    // 나가기 버튼 비활성화(TODO : 만약 무기 선택 중에 나가기가 가능하면 이거 없애야 함)
                    _exitView.ExitButtonInteractable(false);
                }
            });
    }

    // 게임 시작 메서드
    private void StageStart()
    {
        SceneManager.LoadScene($"{_stageSelectData.StageData.stageName}");
    }

    // 나가기 요청 처리 메서드
    private void HandleExitRequested()
    {
        Debug.Log("Exit Requested.");

        // 나가기 버튼 클릭 시 현재 UI 비활성화
        _uiStack.Pop().SetActive(false);
        // 스택이 비어있으면 스테이지 선택 완전히 나가기
        if(_uiStack.Count <= 0)
            _stageSelectGameObject.SetActive(false);
        // 아니면 이전 UI 활성화
        else
            _uiStack.Peek().SetActive(true);
    }
}
