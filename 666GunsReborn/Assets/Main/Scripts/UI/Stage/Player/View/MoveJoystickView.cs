using System;
using UnityEngine;

public class MoveJoystickView : MonoBehaviour
{
    // 조이스틱 참조
    [SerializeField] private Joystick joystick;

    // 이동 방향 벡터
    private Vector3 direction = Vector3.zero;

    // 중재자
    public MoveJoystickPresenter Presenter { get; private set; }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        Presenter = new MoveJoystickPresenter(this);
    }

    private void FixedUpdate()
    {
        // 조이스틱 입력에 따른 방향 벡터
        direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        // 키보드 입력에 따른 방향 벡터
        direction += new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // 이동
        direction.Normalize();

        //Presenter.OnMove?.Invoke(direction);
        Presenter.OnMove?.Invoke(direction);
    }
}
