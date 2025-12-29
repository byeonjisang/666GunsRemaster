using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectView : MonoBehaviour
{
    // 무기 UI 프리팹
    [Header("Weapon Button Prefab")]
    [SerializeField] private GameObject _weaponButtonPrefab;

    // 무기 버튼 부모 오브젝트
    [Header("Weapon Button Parent")]
    [SerializeField] private List<Transform> _weaponButtonParents;

    // 무기 설명 UI
    [Header("Weapon Description UI")]
    [SerializeField] private Image _weaponImage;
    [SerializeField] private Text _weaponPowerText;
    [SerializeField] private Text _weaponDescriptionText;

    // 무기 선택 버튼
    [Header("Weapon Select Button")]
    [SerializeField] private Button _selectWeaponButton;

    // WeaponSelect 중재자
    public WeaponSelectPresenter Presenter { get; private set; }

    /// <summary>
    /// 초기화 메서드
    /// </summary>
    /// <param name="allWeaponsData"></param>
    public void Init(List<WeaponData> allWeaponsData)
    {
        Presenter = new WeaponSelectPresenter(this);

        // 무기 버튼 생성
        CreateWeaponButtons(allWeaponsData);
        // 무기 선택 버튼 이벤트 등록
        _selectWeaponButton.onClick.AddListener(() =>
        {
            Debug.Log("Weapon Selected and Confirmed.");
            Presenter.OnWeaponSelected?.Invoke(allWeaponsData.Find(w => w.weaponSprite == _weaponImage.sprite));
        });
    }
    
    // 선택된 무기들 이미지 및 설명 업데이트 메서드
    public void UpdateWeaponInfo(WeaponData weaponData)
    {
        _weaponImage.sprite = weaponData.weaponSprite;
        _weaponImage.SetNativeSize();
        _weaponPowerText.text = "Power: " + weaponData.power.ToString();
        _weaponDescriptionText.text = weaponData.weaponDescription;
    }

    // 무기 버튼 생성 메서드
    private void CreateWeaponButtons(List<WeaponData> allWeaponsData)
    {
        foreach (var weaponData in allWeaponsData)
        {
            // 버튼 생성 및 초기화
            int index = (int)weaponData.weaponType;
            GameObject weaponButtonObject = Instantiate(_weaponButtonPrefab, _weaponButtonParents[index]);
            WeaponSelectButton weaponButton = weaponButtonObject.GetComponent<WeaponSelectButton>();
            weaponButton.Init(weaponData);

            // 버튼 클릭 시 무기 정보 보여주기
            weaponButton.GetComponent<Button>().onClick.AddListener(() =>
            {
               Presenter.ClickWeaponImage(weaponData);
            });
        }
    }
}
