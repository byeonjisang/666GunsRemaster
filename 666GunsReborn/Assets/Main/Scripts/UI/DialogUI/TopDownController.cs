using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action _onInteractEvent;

    public void CallInteractEvent()
    {
        _onInteractEvent?.Invoke();
    }
}
