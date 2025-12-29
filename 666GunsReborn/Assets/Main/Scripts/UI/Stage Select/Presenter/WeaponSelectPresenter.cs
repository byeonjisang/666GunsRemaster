using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectPresenter
{
    // 무기 선택 후 이벤트
    public System.Action<WeaponData> OnWeaponSelected;

    // 뷰
    private WeaponSelectView _view;

    public WeaponSelectPresenter(WeaponSelectView view)
    {
        _view = view;
    }

    public void ClickWeaponImage(WeaponData weaponData)
    {
        // 무기 선택 처리 로직 추가
        _view.UpdateWeaponInfo(weaponData);
    }
}
