using System;
using UnityEngine;

public class DashButtonPresenter
{   
    // 대쉬 버튼 쿨릭 이벤트
    public Action OnClick;

    // 뷰
    private DashButtonView _view;

    // 생성자
    public DashButtonPresenter(DashButtonView view)
    {
        _view = view;
    }
}