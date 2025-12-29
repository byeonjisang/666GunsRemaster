using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipPresenter
{
    // 무기 장착 변경 이벤트
    public System.Action<int> WeaponEquipChanged;
    // 게임 시작 이벤트 추가 이벤트
    public System.Action StageStartRequested;

    // 뷰
    private WeaponEquipView _view;

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="view"></param>
    public WeaponEquipPresenter(WeaponEquipView view)
    {
        _view = view;
    }

    /// <summary>
    /// 무기 장착 변경 처리 메서드
    /// </summary>
    /// <param name="index"></param>
    public void OnWeaponEquipChanged(int index)
    {
        //Todo: 무기 장착 변경 처리 로직 추가
        WeaponEquipChanged?.Invoke(index);
    }
}
