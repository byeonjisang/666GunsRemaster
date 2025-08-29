using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerActionEvent
{
    public static Action<Vector3> OnMovePress;
    public static Action OnAttackPress;
    public static Action OnAttackReleased;
    public static Action OnDashPress;
}
