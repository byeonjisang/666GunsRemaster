using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponStats : MonoBehaviour
{
    private Sprite weaponSprite;
    public Sprite WeaponSprite { get {return weaponSprite; } private set { } }
    private float damage;
    public float Damage { get {return damage; } private set { } }
    private float bulletSpeed;
    public float BulletSpeed { get {return bulletSpeed; } private set { } }
    private float fireDistance;
    public float FireDistance { get {return fireDistance; } private set { } }
    private float currentReloadTime;
    private float reloadTime;
    private int currentMagazine;
    public int CurrentMagazine { get {return currentMagazine; } private set { } }
    private int maxMagazine;
    private int currentAmmo;
    public int CurrentAmmo { get {return currentAmmo; } private set { } }
    private int maxAmmo;
    private BulletType bulletType;

    public void Initialized(WeaponData weaponData)
    {
        weaponSprite = weaponData.weaponSprite;
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

    private void Reload()
    {
        currentMagazine = maxMagazine;
        currentAmmo -= maxMagazine;
        WeaponManager.Instance.OnUpdateAmmoUI?.Invoke(currentAmmo, currentMagazine);
    }
}