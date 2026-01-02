using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireButtonView : MonoBehaviour
{   
    [Header("Fire Button UI")]
    [SerializeField] private EventTrigger _eventTrigger;

    // 중재자
    public FireButtonPresenter Presenter { get; private set;}

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        Presenter = new FireButtonPresenter(this);

        // 트리거 초기화
        _eventTrigger.triggers.Clear();
        
        // 포인터 다운 이벤트 추가
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => Presenter.OnFirePointerDown?.Invoke());
        _eventTrigger.triggers.Add(entry);

        // 포인터 업 이벤트 추가
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerUp;
        exit.callback.AddListener((data) => Presenter.OnFirePointerUp?.Invoke());
        _eventTrigger.triggers.Add(exit);
    }
}
