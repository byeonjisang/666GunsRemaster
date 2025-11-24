<<<<<<< HEAD
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireButtonView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{   
    // PointerDown 이벤트
    public event Action OnFirePointerDown;
    // PointerUp 이벤트
    public event Action OnFirePointerUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnFirePointerDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnFirePointerUp?.Invoke();
    }
}
=======
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireButtonView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{   
    // PointerDown 이벤트
    public event Action OnFirePointerDown;
    // PointerUp 이벤트
    public event Action OnFirePointerUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnFirePointerDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnFirePointerUp?.Invoke();
    }
}
>>>>>>> origin/main
