using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponStats : MonoBehaviour
{
    private float damage;
    public float Damage { get {return damage; } private set { } }
    private float bulletSpeed;
    public float BulletSpeed { get {return bulletSpeed; } private set { } }
    private float fireDistance;
    public float FireDistance { get {return fireDistance; } private set { } }
    private float currentReloadTime;
    private float reloadTime;
    private int currentMagazine;
    private int maxMagazine;
    private int currentAmmo;
    private int maxAmmo;
    private BulletType bulletType;

    public void Initialized(WeaponData weaponData)
    {
        damage = weaponData.damage;
        bulletSpeed = weaponData.bulletSpeed;
        fireDistance = weaponData.fireDistance;
        reloadTime = weaponData.reloadTime;
        maxMagazine = weaponData.maxMagazine;
        maxAmmo = weaponData.maxAmmo;
        bulletType = weaponData.bulletType;

        currentMagazine = maxMagazine;
        currentAmmo = maxAmmo;
        currentReloadTime = 0f;
    }

    private void Update()
    {
        if(currentReloadTime > 0)
        {
            currentReloadTime -= Time.deltaTime;
            if(currentReloadTime <= 0){
                Reload();
            }
        }
    }

    public bool IsReloading()
    {
        return currentReloadTime > 0;
    }

    public void Fire(){
        currentMagazine--;
        if(currentMagazine <= 0){
            currentReloadTime = reloadTime;
        }
    }

    private void Reload(){
        currentMagazine = maxMagazine;
        currentAmmo -= maxMagazine;
    }
}