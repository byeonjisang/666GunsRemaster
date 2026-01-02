using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashButtonView : MonoBehaviour
{
    [Header("Dash UI")]
    [SerializeField] private Button button;
    [SerializeField] private Image coolDownImage;

    // 중재자
    public DashButtonPresenter Presenter { get; private set;}

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        Presenter = new DashButtonPresenter(this);
    }
}
