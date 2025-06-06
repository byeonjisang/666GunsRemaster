﻿using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Datas/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public WeaponType weaponType;
    public Sprite weaponSprite;
    public string weaponName;
    public float damage;
    public float fireDistance;
    public float reloadTime;
    public int maxMagazine;
    public float bulletSpeed;
    public float defense;
    public float sheld;
    public BulletType bulletType;
}