using System;
using UnityEngine;

public class MoveJoystickPresenter
{
    // 이동 이벤트
    public Action<Vector3> OnMove;

    // 뷰
    private MoveJoystickView _view;

    // 생성자
    public MoveJoystickPresenter(MoveJoystickView view)
    {
        _view = view;
    }
}