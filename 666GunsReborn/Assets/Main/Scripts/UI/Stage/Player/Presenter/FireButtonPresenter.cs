using System;
using UnityEngine;

public class FireButtonPresenter
{   
    // PointerDown 이벤트
    public Action OnFirePointerDown;
    // PointerUp 이벤트
    public Action OnFirePointerUp;

    // 뷰
    private FireButtonView _view;

    // 생성자
    public FireButtonPresenter(FireButtonView view)
    {
        _view = view;
    }
}