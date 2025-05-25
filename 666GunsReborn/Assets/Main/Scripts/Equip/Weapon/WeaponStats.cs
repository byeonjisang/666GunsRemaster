using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponStats : MonoBehaviour
{
    private int index;

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
    public int MaxMagazine { get {return maxMagazine; } private set { } }
    private BulletType bulletType;

    public void Initialized(int index, WeaponData weaponData)
    {
        this.index = index;
        weaponSprite = weaponData.weaponSprite;
        damage = weaponData.damage;
        bulletSpeed = weaponData.bulletSpeed;
        fireDistance = weaponData.fireDistance;
        reloadTime = weaponData.reloadTime;
        maxMagazine = weaponData.maxMagazine;

        bulletType = weaponData.bulletType;

        currentMagazine = maxMagazine;
        currentReloadTime = 0f;
    }

    private void Update()
    {
        if(currentReloadTime > 0)
        {
            currentReloadTime -= Time.deltaTime;
            WeaponManager.Instance.OnUpdateReloadSlider?.Invoke(index, reloadTime, currentReloadTime);
            if (currentReloadTime <= 0)
            {
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
        WeaponManager.Instance.OnUpdateBulletUI?.Invoke(index, maxMagazine, currentMagazine);
    }
}