using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEquipView : MonoBehaviour
{
    [Header("Weapon Equip Images")]
    [SerializeField] private Image _weapon1Image;
    [SerializeField] private Image _weapon2Image;

    [Header("Weapon Equip Button")]
    [SerializeField] private Button _weapon1Button;
    [SerializeField] private Button _weapon2Button;

    [Header("Game Start Button")]
    [SerializeField] private Button _gameStartButton;

    // 중재자
    public WeaponEquipPresenter Presenter { get; private set; }

    public void Init(Weapon.WeaponID[] weaponIDs)
    {
        Presenter = new WeaponEquipPresenter(this);

        // 선택한 무기 이미지 설정
        UpdateWeaponEquip(weaponIDs);

        // 버튼 클릭 리스너 설정 (무기는 1,2번 이지만 인덱스는 0,1번)
        _weapon1Button.onClick.AddListener(() => Presenter.OnWeaponEquipChanged(0));
        _weapon2Button.onClick.AddListener(() => Presenter.OnWeaponEquipChanged(1));

        // 게임 시작 버튼 클릭 리스너 설정
        _gameStartButton.onClick.AddListener(() =>
        {
            Debug.Log("Game Start Button Clicked.");
            Presenter.StageStartRequested?.Invoke();
        });
    }

    public void UpdateWeaponEquip(Weapon.WeaponID[] weaponIDs)
    {
        // 무기 장착 이미지 업데이트 로직 추가
        AddressablesLoader.LoadAssetByLabel<WeaponData>("WeaponData", (weaponDatas) =>
            {
                if(weaponDatas != null)
                {
                    Debug.Log("Weapon Data Loaded: " + weaponDatas.Count);
                    _weapon1Image.sprite = weaponDatas.Find(w => w.weaponID == weaponIDs[0]).weaponSprite;
                    _weapon1Image.SetNativeSize();
                    _weapon2Image.sprite = weaponDatas.Find(w => w.weaponID == weaponIDs[1]).weaponSprite;
                    _weapon2Image.SetNativeSize();
                }
            });
    }
}