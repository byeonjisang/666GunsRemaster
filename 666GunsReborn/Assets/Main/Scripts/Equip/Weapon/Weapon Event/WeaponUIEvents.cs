using System;
using Unity.Burst;
using UnityEngine;

public static class WeaponUIEvents
{
    public static Action<int, int, int> OnUpdateBulletUI;
    public static Action<int, float, float> OnUpdateReloadSlider;
    public static Action<Sprite, Sprite> OnUpdateWeaponImage;
    public static Action OnSwitchWeaponUI;
    public static Action<float> OnUpdateCooldownUI;
}