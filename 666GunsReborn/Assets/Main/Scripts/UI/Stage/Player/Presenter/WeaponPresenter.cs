using System;
using UnityEngine;

public class WeaponPresenter
{   
    // 무기 변경 클릭 이벤트
    public Action OnClick;

    // 뷰
    private WeaponView _view;

    // 생성자
    public WeaponPresenter(WeaponView view)
    {
        _view = view;
    }
}